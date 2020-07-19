using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Tropical.Infrastructure.Filters
{
    public class MvcFormChanged : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            //sbJavaScript.AppendLine("   $(window).bind('beforeunload', function() {");
            //sbJavaScript.AppendLine("       if (changeFormField) {");
            //sbJavaScript.AppendLine("           return 'Existem informações que foram alteradas e não foram gravadas.';");
            //sbJavaScript.AppendLine("       }");
            //sbJavaScript.AppendLine("   });");


            StringBuilder sbJavaScript = new StringBuilder();
            sbJavaScript.AppendLine("        <input class=\"btn\" type=\"button\" href=\"#myModalPageUnload\" data-toggle=\"modal\" style=\"display: none\" id=\"btnPageUnloadOpen\" />");
            sbJavaScript.AppendLine("        <div id=\"myModalPageUnload\" class=\"modal hide fade\" tabindex=\"-1\" role=\"dialog\"");
            sbJavaScript.AppendLine("            aria-labelledby=\"myModalLabelPageUnload\" aria-hidden=\"true\">");
            sbJavaScript.AppendLine("            <div class=\"modal-header\">");
            sbJavaScript.AppendLine("                <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">");
            sbJavaScript.AppendLine("                    ×</button>");
            sbJavaScript.AppendLine("                <h3 id=\"myModalLabelPageUnload\">");
            sbJavaScript.AppendLine("                    Sair da página atual</h3>");
            sbJavaScript.AppendLine("            </div>");
            sbJavaScript.AppendLine("            <div class=\"modal-body\">");
            sbJavaScript.AppendLine("                <p>");
            sbJavaScript.AppendLine("                    Existem informações que foram alteradas e não foram gravadas.<br />");
            sbJavaScript.AppendLine("                    Se você sair sem Salvar essas informações, as mesmas serão perdidas.");
            sbJavaScript.AppendLine("                </p>");
            sbJavaScript.AppendLine("                <p>");
            sbJavaScript.AppendLine("                    Deseja continuar?</p>");
            sbJavaScript.AppendLine("            </div>");
            sbJavaScript.AppendLine("            <div class=\"modal-footer\">");
            sbJavaScript.AppendLine("                <button class=\"btn\" data-dismiss=\"modal\" aria-hidden=\"true\">");
            sbJavaScript.AppendLine("                    Não</button>");
            sbJavaScript.AppendLine("                <button class=\"btn\" data-dismiss=\"modal\" id=\"btnPageUnload\">");
            sbJavaScript.AppendLine("                    Sim</button>");
            sbJavaScript.AppendLine("            </div>");
            sbJavaScript.AppendLine("        </div>");            
            sbJavaScript.AppendLine("        <script language=\"javascript\" type=\"text/javascript\">");
            sbJavaScript.AppendLine("            var changeFormField = false;");
            sbJavaScript.AppendLine("            function OpenModalPageUnload(href, target) {");
            sbJavaScript.AppendLine("                if (typeof changeFormField != \"undefined\") {");
            sbJavaScript.AppendLine("                    if (!changeFormField) {");
            sbJavaScript.AppendLine("                        if (target == \"\") {");
            sbJavaScript.AppendLine("                            window.location.href = href;");
            sbJavaScript.AppendLine("                        } else {");
            sbJavaScript.AppendLine("                            window.open(href, target);");
            sbJavaScript.AppendLine("                        }");
            sbJavaScript.AppendLine("                    } else {");
            sbJavaScript.AppendLine("                        $(\"#btnPageUnload\").attr('onclick', '').unbind('click');");
            sbJavaScript.AppendLine("                        $(\"#btnPageUnload\").click(function () {");
            sbJavaScript.AppendLine("                            if (target == \"\") {");
            sbJavaScript.AppendLine("                                window.location.href = href;");
            sbJavaScript.AppendLine("                            } else {");
            sbJavaScript.AppendLine("                                window.open(href, target);");
            sbJavaScript.AppendLine("                            }");
            sbJavaScript.AppendLine("                        });");
            sbJavaScript.AppendLine("                        $(\"#btnPageUnloadOpen\").trigger(\"click\");");
            sbJavaScript.AppendLine("                    }");
            sbJavaScript.AppendLine("                }");
            sbJavaScript.AppendLine("            }");
            sbJavaScript.AppendLine("            $(document).ready(function () {");
            sbJavaScript.AppendLine("                $(\"a\").each(function (index) {");
            sbJavaScript.AppendLine("                    if ((($(this).attr(\"FormValidate\") == undefined || $(this).attr(\"FormValidate\") == \"true\")) && $(this).attr(\"href\") != undefined && $(this).attr(\"target\") == undefined) {");
            sbJavaScript.AppendLine("                        if ($(this).attr(\"href\").indexOf(\"#\") != 0 && $(this).attr(\"href\").indexOf(\"javascript\") < 0) {");
            sbJavaScript.AppendLine("                            target = '';");
            sbJavaScript.AppendLine("                            if ($(this).attr(\"target\") != undefined) {");
            sbJavaScript.AppendLine("                                target = $(this).attr(\"target\");");
            sbJavaScript.AppendLine("                                $(this).removeAttr(\"target\");");
            sbJavaScript.AppendLine("                            }");
            sbJavaScript.AppendLine("                            $(this).attr(\"href\", \"javascript:OpenModalPageUnload('\" + $(this).attr(\"href\") + \"', '\" + target + \"')\");");
            sbJavaScript.AppendLine("                        }");
            sbJavaScript.AppendLine("                    }");
            sbJavaScript.AppendLine("                });");
            sbJavaScript.AppendLine("               $(\"form\").each(function (index) {");
            sbJavaScript.AppendLine("                   for(i = 0; i < this.elements.length; i++) {");
            sbJavaScript.AppendLine("                      var currElement = $(this.elements[i]);");
            sbJavaScript.AppendLine("                        switch(currElement.get(0).tagName) {");
            sbJavaScript.AppendLine("                            case \"SELECT\" :");
            sbJavaScript.AppendLine("                             currElement.change(function() {");
            sbJavaScript.AppendLine("                                  changeFormField = true;");
            sbJavaScript.AppendLine("                               });");
            sbJavaScript.AppendLine("                               break;");
            sbJavaScript.AppendLine("                           case \"INPUT\" :");
            sbJavaScript.AppendLine("                               if (currElement.attr(\"type\") == \"text\"){");
            sbJavaScript.AppendLine("                                   currElement.keydown(function(){ ");
            sbJavaScript.AppendLine("                                       changeFormField = true;");
            sbJavaScript.AppendLine("                                   }); ");
            sbJavaScript.AppendLine("                               }");
            sbJavaScript.AppendLine("                               if (currElement.attr(\"type\") == \"checkbox\" || currElement.attr(\"type\") == \"radio\"){");
            sbJavaScript.AppendLine("                                   currElement.change(function(){ ");
            sbJavaScript.AppendLine("                                       changeFormField = true;");
            sbJavaScript.AppendLine("                                   }); ");
            sbJavaScript.AppendLine("                               }");
            sbJavaScript.AppendLine("                                break;");
            sbJavaScript.AppendLine("                           case \"TEXTAREA\" :");
            sbJavaScript.AppendLine("                                currElement.keydown(function(){ ");
            sbJavaScript.AppendLine("                                 changeFormField = true;");
            sbJavaScript.AppendLine("                               }); ");
            sbJavaScript.AppendLine("                               break;");
            sbJavaScript.AppendLine("                       }");
            sbJavaScript.AppendLine("                   }");
            sbJavaScript.AppendLine("               });");
            sbJavaScript.AppendLine("           });");
            sbJavaScript.AppendLine("</script>");

            //filterContext.HttpContext.Response.Write(sbJavaScript.ToString());
            filterContext.Controller.ViewBag.MvcFormChanged = sbJavaScript.ToString();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }
    }

}
