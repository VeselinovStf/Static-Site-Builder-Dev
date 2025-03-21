﻿using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.SiteTypes.DTOs;
using Infrastructure.SiteTypes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SiteTypes
{
    public class SiteTypesService : ISiteTypesService<SiteTypeDTO>
    {
        private readonly IAppSiteTypesService<SiteType> appSiteTypeService;
        private readonly IAppProjectsService<Project> appProjectService;
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService;
        private readonly IAppProjectCalculatorService appProjectCalculator;
        private readonly IAppWidgetCalculatorService appWidgetCalculatorService;
        private readonly IAppWidgetService appWidgetService;
        private readonly Dictionary<SiteTypesEnum, SiteTypesFactory> _factories;

        public SiteTypesService(
            IAppSiteTypesService<SiteType> appSiteTypeService,
            IAppProjectsService<Project> appProjectService,
            IAppClientWidgetService appClientWidgetService,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService,
            IAppProjectCalculatorService appProjectCalculator,
            IAppWidgetCalculatorService appWidgetCalculatorService,
            IAppWidgetService appWidgetService

            )
        {
            this.appSiteTypeService = appSiteTypeService ?? throw new System.ArgumentNullException(nameof(appSiteTypeService));
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
            this.appProjectCalculator = appProjectCalculator ?? throw new ArgumentNullException(nameof(appProjectCalculator));
            this.appWidgetCalculatorService = appWidgetCalculatorService ?? throw new ArgumentNullException(nameof(appWidgetCalculatorService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
            _factories = new Dictionary<SiteTypesEnum, SiteTypesFactory>
                        {
                            { SiteTypesEnum.BlogType, new BlogTypeSiteFactory(appProjectService) },
                            { SiteTypesEnum.StoreType, new StoreTypeSiteFactory(appProjectService) }
                        };
        }

        private async Task<bool> ExecuteCreation(SiteTypesEnum action, string clientProjectId,
            string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository, string siteTypeId)
        {
            //Get useble widgets for current template
            var templateUsableWidgets = await this.appSiteTemplateService.GetByTemplateNameAsync(templateName);

            Validator.ObjectIsNull(
                templateUsableWidgets, $"{nameof(SiteTypesService)} : {nameof(ExecuteCreation)} : {nameof(templateUsableWidgets)} : {templateName} -> FATAL : Can't find template useble widgets");

            //Useble id's'
            var usebleWidgetsId = templateUsableWidgets.SiteType.UsebleWidjets.Select(w => w.WidgetId).ToList();

            //All system widgets
            var systemWidgetsCall = await this.appWidgetService.GetAllWidgetsAsync();

            //Get new widget
            var newWidgets = systemWidgetsCall.Where(w => usebleWidgetsId.Contains(w.Id));

            var CanBuyWithDiamonds = await this.appProjectCalculator.CheckDiamondsAsync(clientId, buildInType, templateName, siteTypeId);


            var clientWidgets = await this.appClientWidgetService.GetAllAsync(clientId);

            if (CanBuyWithDiamonds)
            {
                var clientWidgetsId = clientWidgets.ClientWidgets.Select(w => w.WidgetId);

                var widgetsToBuy = systemWidgetsCall.Where(w => !clientWidgetsId.Contains(w.Id));

                if (widgetsToBuy != null)
                {
                    //check for widget bue
                    var widgetsSum = widgetsToBuy.Sum(w => w.Price);

                    var CanByeWithTokens = await this.appWidgetCalculatorService.CheckTakeTokensAsync(clientId, widgetsSum);
                    //if is posible bye

                    if (CanByeWithTokens)
                    {
                        foreach (var widget in widgetsToBuy)
                        {
                            var result = await this.appWidgetCalculatorService.TakeTokensAsync(clientId, widget.Id);

                            if (result)
                            {
                                //add to client widget
                                await this.appClientWidgetService.AddWidget(widget.Id, clientId);

                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                         
                    }
                    else
                    {
                        return false;
                    }
                }

                bool DiamondsBuy = await this.appProjectCalculator.TakeDiamondsAsync(clientId, buildInType, templateName, siteTypeId);

                if (DiamondsBuy)
                {
                    await _factories[action].Create(clientProjectId,
                        name, description, clientId,
                        buildInType, templateName,
                        cardApiKey, cardServiceGate, hostingServiceGate,
                        repository, newWidgets);

                    return true;
                }


                return false;
            }
            else
            {
                return false;
            }


        }

        public async Task<IEnumerable<SiteTypeDTO>> GetAllTypesWithWidgetsAsync(string clientId)
        {
            try
            {
                var siteTypes = await this.appSiteTypeService.GetAllWithWidgetsAsync();


                Validator.ObjectIsNull(
                 siteTypes, $"{nameof(SiteTypesService)} : {nameof(GetAllTypesWithWidgetsAsync)} : {nameof(siteTypes)} : Can't find build in site types!");

                var widgets = await this.appWidgetService.GetAllWidgetsAsync();

                var clientWidgets = await this.appClientWidgetService.GetAllAsync(clientId);


                var serviceModel = new List<SiteTypeDTO>(siteTypes.Select(t => new SiteTypeDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.Type.ToString(),
                    Price = t.Price,
                    SiteTypeWidget = new List<SiteTypeWidgetDTO>(t.UsebleWidjets.Select(w => new SiteTypeWidgetDTO()
                    {
                        WidgetId = w.WidgetId,
                        WidgetName = widgets.FirstOrDefault(b => b.Id == w.WidgetId).Name,
                        Price = widgets.FirstOrDefault(b => b.Id == w.WidgetId).Price,
                        IsAvailible =
                               clientWidgets.ClientWidgets.FirstOrDefault(cw => cw.WidgetId == w.WidgetId) == null ? false : true
                    }))
                }));

                return serviceModel;
            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceGetAllTypesException($"{nameof(SiteTypesServiceGetAllTypesException)} : Can't get build in types! : {ex.Message}");
            }
        }


        public async Task<bool> ConfirmTypeAsync(string buildInType)
        {
            try
            {
                var siteTypes = await this.appSiteTypeService.GetAllWithWidgetsAsync();

                Validator.ObjectIsNull(
                 siteTypes, $"{nameof(SiteTypesService)} : {nameof(ConfirmTypeAsync)} : {nameof(siteTypes)} : Can't find build in site types!");

                if (siteTypes.Any(t => t.Type.ToString() == buildInType))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceConfirmTypeException($"{nameof(SiteTypesServiceConfirmTypeException)} : Can't get build in types! USER TRY UNLEAGLE ACTION : {ex.Message}");
            }
        }

        public async Task<bool> CreateAsync(
            string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
                 name, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(name)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                description, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(description)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(clientId)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                buildInType, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(buildInType)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                templateName, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(templateName)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                cardApiKey, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(cardApiKey)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                cardServiceGate, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(cardServiceGate)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                hostingServiceGate, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(hostingServiceGate)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                repository, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(repository)} : is null/empty");
            Validator.StringIsNullOrEmpty(
               siteTypeId, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var type = (SiteTypesEnum)Enum.Parse(typeof(SiteTypesEnum), buildInType);

                var clientProject = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProject, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(clientProject)} : Object is null");

                var clientProjectId = clientProject.Id;

                Validator.StringIsNullOrEmpty(
                    clientProjectId, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(clientProjectId)} : is null/empty");

                var result = await this.ExecuteCreation(type, clientProjectId,
                           name, description, clientId,
                           buildInType, templateName,
                           cardApiKey, cardServiceGate, hostingServiceGate,
                           repository, siteTypeId);

                return result;

            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceCreateException($"{nameof(SiteTypesServiceCreateException)} : Can't create site type! : {ex.Message}");
            }
        }


    }
}