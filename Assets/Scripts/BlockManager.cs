using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Dictionary <GameObject, Block> blockDict = new Dictionary<GameObject, Block> ();
    // Singleton
    private static BlockManager _instance;
    public static BlockManager instance {get {return _instance;}}

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        }

        else {
            Destroy (this);
        }

        InitBlocks ();
    }

    void InitBlocks () {
        GameObject [] blockObjects = GameObject.FindGameObjectsWithTag ("Block");

        foreach (GameObject block in blockObjects) {
            Block currBlock = block.GetComponent <Block> ();

            if (currBlock) {
                currBlock.InitBlock();
                blockDict.Add (block, currBlock);
            }
        }

        // Debug.Log ("Initializing blocks...");
    }

    public void UpdateBlocks (GameObject block, bool active) {

        if (block == null) {
            return;
        }

        if (blockDict.ContainsKey (block)) {
            block.SetActive (active);

            // Debug.Log (block.name + "'s state set to: " + active);
        }
    }

    public void ResetBlocks () {

        foreach (GameObject block in blockDict.Keys) {
            // Move Block to initial position & rotation, zero out velocities.
            blockDict [block].ResetBlock();

            // Toggle Block back on.
            block.SetActive (true);
        }

        Debug.Log ("Resetting blocks...");
    }
}
