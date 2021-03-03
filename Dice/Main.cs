using System.Resources;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

[assembly: NeutralResourcesLanguage("en-US")]

namespace Dice
{
    class TizenApplication : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            using (var tizenApplication = new TizenApplication())
            {
                Forms.Init(tizenApplication);
                FormsCircularUI.Init();
                tizenApplication.Run(args);
            }
        }
    }
}
