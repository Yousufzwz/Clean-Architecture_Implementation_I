using Autofac;
using PremiumAccess.Web.Areas.Admin.Models;

namespace PremiumAccess.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ContentCreateModel>().AsSelf();
        builder.RegisterType<ContentListModel>().AsSelf();
        builder.RegisterType<ContentUpdateModel>().AsSelf();
    }
}
