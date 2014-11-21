using System;
using System.Reflection;

namespace StefanoVuerich.TestFinale.Portal.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}