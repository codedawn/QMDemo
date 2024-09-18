using QM;
using System;
using UnityEngine;

public class Network : MonoBehaviour
{
    private Client _client = new Client();
    public Room room;
    static Network()
    {
        Client.SetNLogConfigPath(Application.streamingAssetsPath + "/Log/NLog.config");
    }

    async void Awake()
    {
        _client.SetOnPushCallback(OnPush);
        _client.SetOnDisConnectCallback(OnDisConnect);
        await _client.ConnectAsync("127.0.0.1", 20000);
        UnityEngine.Debug.Log("连接成功");
        //_client.Connect("127.0.0.1", 20000);
        System.Random random = new System.Random();
        UserAuthRequest userAuthRequest = new UserAuthRequest() { Id = IdGenerator.NextId(), Token = "test" };
        var response = await _client.SendRequestAsync(userAuthRequest);
        if (!response.IsSuccess())
        {
            Debug.Log(response);
        }
        UserJoinRequest userJoinRequest = new UserJoinRequest() { Id = IdGenerator.NextId(), UserId = 1 };
        var response1 = await _client.SendRequestAsync(userJoinRequest);
        if (!response1.IsSuccess())
        {
            Debug.Log(response1);
        }
        UserJoinResponse userJoinResponse = response1 as UserJoinResponse;
        Debug.Log(userJoinResponse);
        //Stopwatch stopwatch = Stopwatch.StartNew();
        //int count = 1;
        //Task[] tasks = new Task[count];
        //for (int i = 0; i < count; i++)
        //{
        //    userRequest.Id = i;
        //    tasks[i] = _client.SendRequestAsync(userRequest);
        //}
        //await Task.WhenAll(tasks);
        //stopwatch.Stop();
        //UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds + "ms");
    }

    private void OnDisConnect()
    {
        Debug.Log("OnDisConnect");
    }

    private void OnPush(IPush push)
    {
        Debug.Log(push);
        MainThreadDispatcher.RunOnMainThread(() =>
        {
            if (push is RoomStatePush roomStatePush)
            {
                foreach (var player in roomStatePush.playerStates)
                {
                    room.SetPlayerState(player);
                }
            }
            else if (push is AddRewardPush addRewardPush)
            {
                room.AddReward(addRewardPush.reward);
            }
            else if (push is TouchRewardPush touchRewardPush)
            {
                room.TouchReward(touchRewardPush);
            }
            else if (push is AllRewardPush allRewardPush)
            {
                foreach (RewardState rewardState in allRewardPush.rewardStates)
                {
                    room.AddReward(rewardState);
                }
            }
        });
    }

    private async void OnDestroy()
    {
        if (_client != null)
        {
            await _client.CloseAsync();
        }
    }
}
