//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Dice.Views.RollPage.xaml", "Views/RollPage.xaml", typeof(global::Dice.Views.RollPage))]

namespace Dice.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\RollPage.xaml")]
    public partial class RollPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label totalLabel;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Tizen.Wearable.CircularUI.Forms.CircleScrollView rollResult;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(RollPage));
            totalLabel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "totalLabel");
            rollResult = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Tizen.Wearable.CircularUI.Forms.CircleScrollView>(this, "rollResult");
        }
    }
}
