using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PrimS.Telnet;

namespace VirtualSports.AndroidEmulator
{
    public class EmulatorController
    {
        private string _host;
        private int _port;
        private string _authToken;
        private CancellationToken _cancellationToken;

        private Client _client;
        public bool Connected => _client is { IsConnected: true };

        public EmulatorController(string host, int port, string authToken = "")
        {
            _host = host;
            _port = port;
            _authToken = authToken;
        }

        public async Task<bool> Connect()
        {
            if (!Connected)
            {
                _cancellationToken = new CancellationToken();
                _client = new Client(_host, _port, _cancellationToken);
            }

            if (Connected)
            {
                await UpdateAuthToken();
            }
            return Connected;
        }

        private async Task UpdateAuthToken()
        {
            string content = await _client.ReadAsync();
            var match = Regex.Match(content, @"Android Console: you can find your <auth_token> in\s+'(.+)'");
            string tokenPath = match.Groups[1].Value;
            if (string.IsNullOrEmpty(_authToken))
            {
                _authToken = File.ReadAllText(tokenPath);
            }
        }

        public async Task<bool> Login()
        {
            var response = await Request($"auth {_authToken}");
            return response.Successful;
        }

        private async Task<EmulatorResponse> Request(string command)
        {
            await _client.WriteLine(command);
            return new EmulatorResponse(await _client.ReadAsync());
        }

        public async Task<bool> UpdateLocation(double lon, double lat)
        {
            var response = await Request($"geo fix {lon} {lat}");
            return response.Successful;
        }

        public async Task<bool> UpdateAcceleration(float ax, float ay, float az)
        {
            var response = await Request("sensor set acceleration " + string.Join(":",
                new[] {ax, ay, az}));
            return response.Successful;
        }
    }
}
