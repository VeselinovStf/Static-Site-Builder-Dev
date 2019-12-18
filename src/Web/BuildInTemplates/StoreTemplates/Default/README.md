# Widgets Description

## Description

    - Widgets integration contains of:
        - template availible for use .json file. - appAvailibleWidgetsConfig.json
            - what widgets are useble for this template
        - template used widgets .json file. - appUsedWidgetsConfig.json
            - what widgets are used by client
            - to set up new widget add from availible to used config files
        - widgets .json description placed in _data/{widget_name}/{widget_name.json}
        - widgets html fillement placed in _includes/widgets/{widget_name}/{widget_name.html}
            - note that the html fillement path is the same as the widget .json .element prop

### Made it work

    - any page must have include to page-structure.html 
        - any include must have param='the page to render'
    - page-structure shell render every widget for use in current page
    - page-structure is getting all useble widgets and sends to widget view page
    - the page that use widget is renderen and is responsible for widget comunication    

### Widgets JSON

    - Types of widgets
<code>
{
    "widgetType" : "simple_dependent",
    "placement" : "top",
    "name": "top-header",
    "display": true,   
    "dependancy" : 
    [
        "language-change", 
        "currency-change", 
        "props-pages", 
        "props-title"
    ]      
}

{
    "widgetType" : "simple_Elements",
    "placement" : "top",
    "name": "currency-change",
    "display": true,
    "elements": [
        {
            "name" : "USD ($)"
        },
        {
            "name" : "EUR (€)"
        }
    ],
    "dependancy" : null    
}

   

{
    "widgetType" : "simple",
    "placement" : "top",
    "name": "top-header-name",
    "display": true,
    "multy" : false,
    "element": "Owsome site name",
    "dependancy" : null    
}
</code>

### Widget html

1. assign widget and dependencies
{% assign widget = site.data.widgets.top-header.top-header %}
{% if widget.dependant %}
    {% assign widgetDependency = widget.dependancy %}
{% endif %}