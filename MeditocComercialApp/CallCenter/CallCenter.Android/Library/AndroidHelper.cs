using System;
using Android.App;
using CallCenter.Renderers;
using Xamarin.Forms;
using static Android.Views.View;

namespace CallCenter.Droid.Library
{
	public static class AndroidHelper
	{


		public static Activity contextActivity { get; set; }
		public static BackButtonClickListener OnClickListener { get; set; }
		public static Xamarin.Forms.Application CurrentApp { get; set; }
		public static int ToolBarId { get; set; }



		public class BackButtonClickListener : Java.Lang.Object, IOnClickListener
		{
			public void OnClick(Android.Views.View view)
			{

				if (CurrentApp != null)
				{
					if (CurrentApp.MainPage.Navigation.NavigationStack.Count > 0)
					{
						int index = CurrentApp.MainPage.Navigation.NavigationStack.Count - 1;

						if (CurrentApp.MainPage.Navigation.NavigationStack[index] is ICoolContentPage)
						{
							var currentPage = CurrentApp.MainPage.Navigation.NavigationStack[index] as ICoolContentPage;

							if (currentPage.EnableBackButtonOverride)
							{
								currentPage?.CustomBackButtonAction.Invoke();
							}
							else
							{
								currentPage.UnSubscribe();
								CurrentApp.MainPage.Navigation.PopAsync();
							}
						}
						else
							CurrentApp.MainPage.Navigation.PopAsync();
					}
					else
					{
						//manejo de master detail page.
						if((CurrentApp.MainPage is MasterDetailPage))
                        {
							var masterPager = CurrentApp.MainPage as MasterDetailPage;
							if (masterPager.Detail is NavigationPage)
							{
								var navigation = masterPager.Detail as NavigationPage;
								if (navigation.CurrentPage is ICoolContentPage)
								{
									var current = navigation.CurrentPage as ICoolContentPage;
									if (current.EnableBackButtonOverride)
									{
										current?.CustomBackButtonAction.Invoke();
									}
									else
									{
										current.UnSubscribe();
										CurrentApp.MainPage.Navigation.PopAsync();
									}
								}
								else CurrentApp.MainPage.Navigation.PopAsync();
							}
							else CurrentApp.MainPage.Navigation.PopAsync();
						}
						else if (CurrentApp.MainPage.Navigation.ModalStack.Count > 0)
						{
							CurrentApp.MainPage.Navigation.PopModalAsync();
						}
					}
				}
			}
		}
	}
}
