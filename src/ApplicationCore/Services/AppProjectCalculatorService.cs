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
       

        public AppProjectCalculatorService(
            IAsyncRepository<Wallet> walletRepository,
            IAsyncRepository<SiteType> siteTypeRepository,
            IAsyncRepository<SiteTemplate> siteTemplateRepository
           )
        {
            this.walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            this.siteTypeRepository = siteTypeRepository ?? throw new ArgumentNullException(nameof(siteTypeRepository));
            this.siteTemplateRepository = siteTemplateRepository ?? throw new ArgumentNullException(nameof(siteTemplateRepository));
           
        }

        public async Task<bool> TakeDiamondsAsync(string clientId, string buildInType, string templateName, string siteTypeId)
        {
            var walletSpecification = new GetWalletByClientIdSpecification(clientId);

            var wallet =  this.walletRepository.GetSingleBySpec(walletSpecification);

            
            //var siteTypeSpecification = new GetSiteTypeBySiteTypeEnum(buildInType);

            var siteType = await this.siteTypeRepository.GetByIdAsync(siteTypeId);


            var templateSpecification = new GetTemplateByNameSpecification(templateName);

            var template = this.siteTemplateRepository.GetSingleBySpec(templateSpecification);         

            //get total price
            var clientWalletDiamonds = wallet.AvailibleDiamons;
           
            var siteTypePrice = siteType.Price;
            var templatePrice = template.Price;          

            var totalPriceDiamonds = siteTypePrice + templatePrice;
            //check if is posible
            var diamondsSub = clientWalletDiamonds - totalPriceDiamonds;
           

            if (diamondsSub > -1)
            {
                             
                    wallet.AvailibleDiamons -= totalPriceDiamonds;
                   
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
