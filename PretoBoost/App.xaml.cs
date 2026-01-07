using System;
using System.Security.Principal;
using System.Windows;

namespace PretoBoost
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Verificar privilégios de administrador
            if (!IsRunAsAdministrator())
            {
                MessageBox.Show(
                    "⚠️ PretoBoost precisa ser executado como Administrador para aplicar as otimizações.\n\n" +
                    "Por favor, clique com o botão direito no executável e selecione 'Executar como administrador'.",
                    "Privilégios Insuficientes",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private static bool IsRunAsAdministrator()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }
    }
}
