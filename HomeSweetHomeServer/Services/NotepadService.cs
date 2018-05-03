using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Repositories;
using HomeSweetHomeServer.Services;
using HomeSweetHomeServer.Exceptions;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HomeSweetHomeServer.Services
{
    public class NotepadService : INotepadService
    {
        public IInformationRepository _informationRepository;
        public IUserRepository _userRepository;
        public IUserInformationRepository _userInformationRepository;
        public IConfiguration _config;
        public IMailService _mailService;
        public IHomeRepository _homeRepository;
        public IFCMService _fcmService;
        public IFriendshipRepository _friendshipRepository;
        public INotepadRepository _notepadRepository;

        public NotepadService(IInformationRepository informationRepository,
                           IUserRepository userRepository,
                           IUserInformationRepository userInformationRepository,
                           IConfiguration config,
                           IMailService mailService,
                           IHomeRepository homeRepository,
                           IFCMService fcmService,
                           IFriendshipRepository friendshipRepository,
                           INotepadRepository notepadRepository)
        {
            _informationRepository = informationRepository;
            _userRepository = userRepository;
            _userInformationRepository = userInformationRepository;
            _config = config;
            _mailService = mailService;
            _homeRepository = homeRepository;
            _fcmService = fcmService;
            _friendshipRepository = friendshipRepository;
            _notepadRepository = notepadRepository;
        }
        
        //Responses client to notepad last update state
        public async Task<List<SynchronizeModel<NotepadModel>>> SynchronizeNotepad(UserModel user, ClientNotepadContextModel client)
        {
            if(user.Position == 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }
                        
            List<SynchronizeModel<NotepadModel>> update = new List<SynchronizeModel<NotepadModel>>();

            List<NotepadModel> serverState = await _notepadRepository.GetAllNoteByHomeId(user.Home.Id);
            List<SyncModel> clientState = client.LastState.OrderBy(n => n.Id).ToList();

            int i = 0, j = 0;

            while(i < serverState.Count && j < clientState.Count)
            {

                if(serverState[i].Id == clientState[j].Id)
                {
                    if(serverState[i].LastUpdated.CompareTo(clientState[j].LastUpdated) == 1)
                    {
                        //Update client
                        update.Add(new SynchronizeModel<NotepadModel>(serverState[i].Id, 2,
                                      new NotepadModel(serverState[i].Id,
                                                       user.Home,
                                                       serverState[i].Title,
                                                       serverState[i].Content,
                                                       serverState[i].LastUpdated)));
                    }
                    i++; j++;
                }
                else if(serverState[i].Id < clientState[i].Id)
                {
                    //Add to client
                    update.Add(new SynchronizeModel<NotepadModel>(serverState[i].Id, 0,
                                      new NotepadModel(serverState[i].Id,
                                                       user.Home,
                                                       serverState[i].Title,
                                                       serverState[i].Content,
                                                       serverState[i].LastUpdated)));
                    i++;
                }
                else
                {
                    //Delete from client
                    update.Add(new SynchronizeModel<NotepadModel>(clientState[j].Id, 1, null));
                    j++;
                }
            }

            while(i < serverState.Count)
            {
                //Add to client
                update.Add(new SynchronizeModel<NotepadModel>(serverState[i].Id, 0,
                                      new NotepadModel(serverState[i].Id,
                                                       user.Home,
                                                       serverState[i].Title,
                                                       serverState[i].Content,
                                                       serverState[i].LastUpdated)));
                i++;
            }

            while(j < clientState.Count)
            {
                //Delete from client
                update.Add(new SynchronizeModel<NotepadModel>(clientState[j].Id, 1, null));
                j++;
            }

            return update;
        }

        public async Task AddNote(UserModel user, NotepadModel note)
        {
            if (user.Position == 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);
            note.Home = user.Home;
            note.LastUpdated = DateTime.UtcNow;

            foreach (var friend in user.Home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type: "NotepadUpdate");
                await _fcmService.SendFCMAsync(fcm);
            }

            await _notepadRepository.InsertAsync(note);
        }

        public async Task DeleteNote(UserModel user, int noteId)
        {
            if (user.Position == 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);

            NotepadModel note = await _notepadRepository.GetNoteById(noteId, true);

            if(note == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Note Not Exist", "Note is not exist");
                errors.Throw();
            }

            if(note.Home != user.Home)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Note Not Belongs Home", "Note does not belong this home");
                errors.Throw();
            }

            foreach (var friend in user.Home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type: "NotepadUpdate");
                await _fcmService.SendFCMAsync(fcm);
            }

            await _notepadRepository.DeleteAsync(note);
        }

        public async Task UpdateNote(UserModel user, NotepadModel note)
        {
            if (user.Position == 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }
            
            user = await _userRepository.GetByIdAsync(user.Id, true);
            note.Home = user.Home;
            note.LastUpdated = DateTime.UtcNow;
            
            foreach (var friend in user.Home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type: "NotepadUpdate");
                await _fcmService.SendFCMAsync(fcm);
            }

            await _notepadRepository.UpdateAsync(note);
        }
    }
}
