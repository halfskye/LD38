using UnityEngine;

using Assets.Scripts;

public class PlayerSpawner : MonoBehaviour {

	private const KeyCode KEY_SPAWN_PLAYER = KeyCode.Alpha5;
    private bool KEY_MOVE_RIGHT() { return (Input.GetKeyDown(KeyCode.JoystickButton0)); }

    [SerializeField]
	private float PLAYER_SPAWN_COST = 50.0f;
	[SerializeField]
	private GameObject _playerPrefab = null;

	private HomeBase _homeBase = null;

	private void Awake() {
		_homeBase = GameObject.FindWithTag(Constants.Tags.HomeBase).GetComponent<HomeBase>();
	}

	// Update is called once per frame
	private void Update () {
		if((Input.GetKeyDown(KEY_SPAWN_PLAYER) || KEY_MOVE_RIGHT()) && _homeBase.HasEnoughResource(PLAYER_SPAWN_COST) && IsPlayerDead()) {
			SpawnPlayer();
		}
	}

    public void RespawnPlayerAudio()
    {
        var respawn = GameObject.Find("AudioController");
        respawn.GetComponent<AudioController>().playerRespawnAudio();
    }

	private bool IsPlayerDead() {
		GameObject player = GameObject.FindWithTag(Constants.Tags.Player);
		return (player == null);
	}

	private void SpawnPlayer() {
            RespawnPlayerAudio();
			GameObject player = (GameObject)Instantiate(_playerPrefab);
			player.transform.position = Vector3.zero + (Vector3.down * 2.0f);

			_homeBase.UseResource(PLAYER_SPAWN_COST);
	}
}
