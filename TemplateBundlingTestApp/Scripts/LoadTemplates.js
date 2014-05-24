function LoadTemplate(templateId, containerId)
{
    var template = document.getElementById(templateId).innerHTML;
    var container = document.getElementById(containerId);
    container.innerHTML = template;
}