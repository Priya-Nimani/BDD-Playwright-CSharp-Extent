using System.Text.Json;

namespace PriyaNewProject.Support
{
    public class EnvironmentSettings
    {
        public string Browser { get; set; } = "chromium";
        public bool Headless { get; set; } = true;
        public string BaseUrl { get; set; } = "";
        public int SlowMo { get; set; } = 0;
    }

    public static class ConfigManager
    {
        public static EnvironmentSettings LoadSettings()
        {
            // Get environment from ENV variable or default to "dev"
            string env = Environment.GetEnvironmentVariable("TEST_ENV") ?? "dev";
            
            // Get absolute path to the config file
            string baseDir = AppContext.BaseDirectory; // bin/Debug/netX.Y/...
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..")); 
            string filePath = Path.Combine(projectRoot, "config", $"{env}_config.json");

            if (!File.Exists(filePath)) 
            {
                throw new FileNotFoundException($"Config file not found for environment: {env} at {filePath}");
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<EnvironmentSettings>(json)
                   ?? new EnvironmentSettings();
        }
    }
}
