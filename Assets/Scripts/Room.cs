using QM;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Playables;

public class Room : MonoBehaviour
{
    private Dictionary<long, Player> _players = new Dictionary<long, Player>();
    public GameObject playerPrefab;
    public GameObject rewardPrefab;
    private Dictionary<long, Reward> _rewards = new Dictionary<long, Reward>();

    public void SetPlayerState(PlayerState playerState)
    {
        if (_players.TryGetValue(playerState.uid, out Player player))
        {
            player.SetPosition(playerState.posX, playerState.posY);
            player.SetPoint(playerState.point);
            player.SetName("player:" + playerState.uid);
        }
        else
        {
            GameObject g = Instantiate(playerPrefab);
            Player p = g.GetComponent<Player>();
            p.SetPosition(playerState.posX, playerState.posY);
            p.SetPoint(playerState.point);
            p.SetName("player:" + playerState.uid);
            _players[playerState.uid] = p;
        }
    }

    public void AddReward(RewardState rewardState)
    {
        GameObject g = Instantiate(rewardPrefab);
        Reward r = g.GetComponent<Reward>();
        r.SetId(rewardState.id);
        r.SetPosition(rewardState.x, rewardState.y);
        _rewards[rewardState.id] = r;
    }

    public void TouchReward(TouchRewardPush touchRewardPush)
    {
        if (_rewards.TryGetValue(touchRewardPush.id, out Reward reward))
            Destroy(reward.gameObject);
        _rewards.Remove(touchRewardPush.id);
    }
}
