using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.Wallet;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppProjectCalculatorService : IAppProjectCalculatorService
    {
        private readonly IAsyncRepository<Wallet> walletRepository;
        private readonly IAsyncRepository<SiteType> siteTypeRepository;
        private readonly IAsyncRepository<SiteTemplate> siteTemplateRepository;
        private readonly IAsyncRepository<Widget> widgetRepository;

        public AppProjectCalculatorService(
            IAsyncRepository<Wallet> walletRepository,
            IAsyncRepository<SiteType> siteTypeRepository,
            IAsyncRepository<SiteTemplate> siteTemplateRepository,
            IAsyncRepository<Widget> widgetRepository)
        {
            this.walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            this.siteTypeRepository = siteTypeRepository ?? throw new ArgumentNullException(nameof(siteTypeRepository));
            this.siteTemplateRepository = siteTemplateRepository ?? throw new ArgumentNullException(nameof(siteTemplateRepository));
            this.widgetRepository = widgetRepository ?? throw new ArgumentNullException(nameof(widgetRepository));
        }

        public async Task<bool> TakeDiamondsAsync(string clientId, string buildInType, string templateName)
        {
            var walletSpecification = new GetWalletByClientIdSpecification(clientId);

            var wallet =  this.walletRepository.GetSingleBySpec(walletSpecification);

            
            var siteTypeSpecification = new GetSiteTypeBySiteTypeEnum(buildInType);

            var siteType = this.siteTypeRepository.GetSingleBySpec(siteTypeSpecification);


            var templateSpecification = new GetTemplateByNameSpecification(templateName);

            var template = this.siteTemplateRepository.GetSingleBySpec(templateSpecification);

            var widgetSpecification = new GetWidgetsBySiteTypeEnumSpecification(buildInType);

            var widgets = await this.widgetRepository.ListAsync(widgetSpecification);


            //get total price
            var clientWalletDiamonds = wallet.AvailibleDiamons;
            var clientWalletTokens = wallet.AvailibleCredit;
            var siteTypePrice = siteType.Price;
            var templatePrice = template.Price;

            var widgetsPrice = widgets.Sum(w => w.Price);

            var totalPriceDiamonds = siteTypePrice + templatePrice;
            //check if is posible
            var diamondsSub = clientWalletDiamonds - totalPriceDiamonds;
            var tokenSub = clientWalletTokens - widgetsPrice;

            if ((diamondsSub > -1) && (tokenSub > -1))
            {
                

              
                    wallet.AvailibleDiamons -= totalPriceDiamonds;
                    wallet.AvailibleCredit -= widgetsPrice;

                    await this.walletRepository.UpdateAsync(wallet);

                    return true;
                
                
            }
            else
            {
                return false;
            }
           

            
        }
    }
}
