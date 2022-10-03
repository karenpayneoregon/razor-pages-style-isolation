﻿
using Microsoft.AspNetCore.Mvc.ApplicationModels;

// ReSharper disable once CheckNamespace
namespace IsolationWebApp;

public partial class Program
{
    private static void Build3(WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages(options =>
        {

            options.Conventions.AddPageRouteModelConvention("/About", model =>
            {
                var selectorCount = model.Selectors.Count;
                for (var i = 0; i < selectorCount; i++)
                {
                    var selector = model.Selectors[i];
                    model.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel
                        {
                            Order = 2,
                            Template = AttributeRouteModel.CombineTemplates(
                                selector.AttributeRouteModel!.Template,
                                "{aboutTemplate?}"),
                        }
                    });
                }
            });
        });
    }
    
}