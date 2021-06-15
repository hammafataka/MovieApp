using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MovieApp.Services;
using System.IO;

namespace MovieApp
{
    public partial class App : Application
    {
        private static sql_service dbContext;

        public static sql_service DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    string dbpath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MovieApp-v01.db3"
                        );
                    dbContext = new sql_service(dbpath);

                }
                return dbContext;

            }

        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
