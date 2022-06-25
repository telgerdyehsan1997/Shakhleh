using AsmApiService;
using Olive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace APIHandler
{

    public interface IASMService : IDisposable
    {
        Task<CreateImportGbDeclarationResponse> CreateShipmentIntoUK(string declaration);
        Task<CreateExportGbDeclarationResponse> CreateShipmentOutOfUK(string declaration);
        Task<AmendAcceptedImportGbDeclarationResponse> AmendShipmentIntoUK(string declaration);
        Task<AmendAcceptedExportGbDeclarationResponse> AmendShipmentOutOfUK(string declaration);
        Task<SendGbDeclarationResponse> SendDeclaration(string declaration);
        Task<SendNctsDeclarationResponse> SendNCTSDeclaration(string declaration);
        Task<SendGbDeclarationCancellationResponse> CancelDeclaration(string declaration);
        Task<CreateNctsDeclarationResponse> CreateNCTSDeclaration(string declaration);
        Task<GetNctsDeclarationStatusResponse> GetNCTSDeclarationStatus(string transactionId);
        Task<GetNctsTransitStatusResponse> GetNCTSTransitStatus(string transactionId);
        Task<GetCustomsReportDeclarationResponseResponse> GetCustomsReportDeclarationResponse(string key);
        Task<GetFrontierDeclarationResponseResponse> GetFrontierDeclarationResponse(string key);


    }

    public class ASMService : IASMService
    {

        private ISequoiaApiSingleChannelService _client;
        private ISequoiaApiSingleChannelService Client
        {
            get
            {
                if (_client == null)
                {
                    var binding = new NetTcpBinding(SecurityMode.None)
                    {
                        TransferMode = TransferMode.Buffered,
                        MaxReceivedMessageSize = 2147483647
                    };
                    var address = new EndpointAddress(Config.Get("Integration:ASM:Url"));
                    var channelFactory = new ChannelFactory<ISequoiaApiSingleChannelService>(binding, address);
                    _client = channelFactory.CreateChannel();
                }
                return _client;
            }
        }


        private async Task<bool> Login()
        {
            var userName = Config.Get("Integration:ASM:Username");
            var password = Config.Get("Integration:ASM:Password");
            var result = await Client.LogonAsync(new LogonRequest(new SequoiaLogonRequest { UserName = userName, Password = password }));
            return result.LogonResult.Authenticated;
        }

        #region EAD
        public async Task<CreateImportGbDeclarationResponse> CreateShipmentIntoUK(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");
            var result = await Client.CreateImportGbDeclarationAsync(new CreateImportGbDeclarationRequest(new CreateRequest { Content = declaration }));
            return result;
        }

        public async Task<CreateExportGbDeclarationResponse> CreateShipmentOutOfUK(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.CreateExportGbDeclarationAsync(new CreateExportGbDeclarationRequest(
                 new CreateRequest { Content = declaration }
            ));

            return result;
        }

        public async Task<SendGbDeclarationResponse> SendDeclaration(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.SendGbDeclarationAsync(new SendGbDeclarationRequest(new SendRequest() { Content = declaration }));

            return result;
        }

        public async Task<SendGbDeclarationCancellationResponse> CancelDeclaration(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");
            var result = await Client.SendGbDeclarationCancellationAsync(new SendGbDeclarationCancellationRequest(new SendRequest() { Content = declaration }));
            return result;
        }


        public async Task<AmendAcceptedImportGbDeclarationResponse> AmendShipmentIntoUK(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");
            var result = await Client.AmendAcceptedImportGbDeclarationAsync(new AmendAcceptedImportGbDeclarationRequest(new UpdateRequest { Content = declaration }));
            return result;
        }

        public async Task<AmendAcceptedExportGbDeclarationResponse> AmendShipmentOutOfUK(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.AmendAcceptedExportGbDeclarationAsync(new AmendAcceptedExportGbDeclarationRequest(
                 new UpdateRequest { Content = declaration }
            ));

            return result;
        }

        #endregion

        #region NCTS
        public async Task<CreateNctsDeclarationResponse> CreateNCTSDeclaration(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.CreateNctsDeclarationAsync(new CreateNctsDeclarationRequest(new CreateRequest { Content = declaration }));

            return result;
        }

        public async Task<SendNctsDeclarationResponse> SendNCTSDeclaration(string declaration)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.SendNctsDeclarationAsync(new SendNctsDeclarationRequest(new SendRequest { Content = declaration }));

            return result;
        }

        public async Task<GetNctsDeclarationStatusResponse> GetNCTSDeclarationStatus(string transactionId)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.GetNctsDeclarationStatusAsync(new GetNctsDeclarationStatusRequest(new GetSpecificRecordRequest { TransactionId = transactionId }));

            return result;
        }

        public async Task<GetNctsTransitStatusResponse> GetNCTSTransitStatus(string transactionId)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.GetNctsTransitStatusAsync(new GetNctsTransitStatusRequest(new GetSpecificRecordRequest { TransactionId = transactionId }));

            return result;
        }

        void IDisposable.Dispose()
        {
            if (_client == null) return;
            bool success = false;
            try
            {
                if (((IClientChannel)_client).State != CommunicationState.Faulted)
                {
                    ((IClientChannel)_client).Close();
                    success = true;
                }
            }
            finally
            {
                if (!success)
                    ((IClientChannel)_client).Abort();

                _client = null;
            }
        }
        #endregion


        public async Task<GetCustomsReportDeclarationResponseResponse> GetCustomsReportDeclarationResponse(string key)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            var result = await Client.GetCustomsReportDeclarationResponseAsync(
                            new GetCustomsReportDeclarationResponseRequest(new GetSpecificRecordRequest
                            {
                                TransactionId = key,
                                NaturalKey = key
                            }));

            return result;
        }

        public async Task<GetFrontierDeclarationResponseResponse> GetFrontierDeclarationResponse(string key)
        {
            if (!await Login()) throw new ValidationException("API Credential is not correct.");

            return await Client.GetFrontierDeclarationResponseAsync(
                            new GetFrontierDeclarationResponseRequest(new GetSpecificRecordRequest
                            {
                                TransactionId = key,
                                NaturalKey = key
                            }));

        }
    }
}
