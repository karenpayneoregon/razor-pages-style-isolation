using Microsoft.AspNetCore.Mvc.ApplicationModels;
#pragma warning disable CS8670

namespace IsolationWebApp.Classes;

/// <summary>
/// Sets up parameter language for all pages
/// </summary>
public class CombinedPageRouteModelConvention : IPageRouteModelConvention
{
    
    private const string BaseUrlTemplateWithoutSegment = "{language?}/";
    private const string BaseUrlTemplateWithSegment = "{language?}/{segment?}/";

    public void Apply(PageRouteModel model)
    {
        var allSelectors = new List<SelectorModel>();
        foreach (var selector in model.Selectors)
        {
            //setup the route with segment
            allSelectors.Add(CreateSelector(selector, BaseUrlTemplateWithSegment));
            //setup the route without segment
            allSelectors.Add(CreateSelector(selector, BaseUrlTemplateWithoutSegment));
        }

        //replace the default selectors with new selectors
        model.Selectors.Clear();
        foreach (var selector in allSelectors)
        {
            model.Selectors.Add(selector);
        }

    }
    private static SelectorModel CreateSelector(SelectorModel defaultSelector, string template)
    {
        var fullTemplate = AttributeRouteModel.CombineTemplates(
            template,
            defaultSelector.AttributeRouteModel!.Template);

        var newSelector = new SelectorModel(defaultSelector)
        {
            AttributeRouteModel = { Template = fullTemplate }
        };

        return newSelector;
    }
}

public class GlobalTemplatePageRouteModelConvention
    : IPageRouteModelConvention
{
    public void Apply(PageRouteModel model)
    {
        var selectorCount = model.Selectors.Count;
        for (var i = 0; i < selectorCount; i++)
        {
            var selector = model.Selectors[i];
            model.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Order = 1,
                    Template = AttributeRouteModel.CombineTemplates(
                        selector.AttributeRouteModel!.Template,
                        "{globalTemplate?}"),
                }
            });
        }
    }
}