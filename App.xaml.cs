using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace UFIP.EngChat
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs eventstart)
        {

            var assemblies = new Dictionary<string, Assembly>();
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resources = executingAssembly.GetManifestResourceNames().Where(n => n.EndsWith(".dll"));

            foreach (string resource in resources)
            {
                using (var stream = executingAssembly.GetManifestResourceStream(resource))
                {
                    if (stream == null)
                        continue;

                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    try
                    {
                        assemblies.Add(resource, Assembly.Load(bytes));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Print(string.Format("Failed to load: {0}, Exception: {1}", resource, ex.Message));
                    }
                }
            }

            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                var assemblyName = new AssemblyName(e.Name);

                var path = string.Format("{0}.dll", assemblyName.Name);

                if (assemblies.ContainsKey(path))
                {
                    return assemblies[path];
                }

                return null;
            };

            
            base.OnStartup(eventstart);
        }

        public void test() { 
            }
        }
    }


