using Dice.Services;
using System;
using Xamarin.Forms;

namespace Dice.Views
{
  public partial class RollPage : ContentPage
  {
    private App app;
    private bool appeared;
    public RollPage()
    {
      appeared = false;
      app = (App)Application.Current;

      InitializeComponent();

      PreparePaging();
      app.Loaded += Loaded;

      PrepareRemoveHandlers();
    }

    private void Loaded(object sender = null, EventArgs e = null)
    {
      SwitchPage(0);
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      appeared = true;
      UpdateDefault();
    }

    protected override void OnDisappearing()
    {
      base.OnAppearing();
      appeared = false;
    }

    private void UpdateRoll()
    {
      RenderResult(set.Roll());
    }

    private void UpdateDefault()
    {
      RenderResult(set.Default());
    }

    public void Clear()
    {
      set.Clear();
      UpdateDefault();
    }

    public void Discard()
    {
      provider.Discard(page);
      if (provider.Count() == 0 || page == 0)
        ShowPage(0);
      else
        PrevPage();
    }

    private void Clear_Click(object sender, EventArgs e)
    {
      Navigation.PushAsync(new ClearPage(this));
    }

    private void Add_Click(object sender, EventArgs e)
    {
      Navigation.PushAsync(new DicePage(set));
    }

    private void Roll_Click(object sender, EventArgs e)
    {
      UpdateRoll();
    }
  }
}
