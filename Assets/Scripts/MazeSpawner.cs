using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject PatrolEnemy = null;
	public GameObject ChaseEnemy = null;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public GameObject Fence = null;
	public GameObject Statue = null;
	public GameObject SpikeTrap = null;
	public GameObject Door = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;
	public GameObject GoalPrefab = null;

	private BasicMazeGenerator mMazeGenerator = null;

	void Start () {
		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}
		mMazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float tempVar = (AddGaps) ? .2f : 0;
				float x = column*(CellWidth+(tempVar));
				float z = row*(CellHeight+(tempVar));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				bool[] wallArray = new bool[4]{ cell.WallBack, cell.WallFront, cell.WallLeft, cell.WallRight };
				int count = 0;
				for (int i = 0; i < 4; i++) {
					if (wallArray [i] == true)
						count++;
				}
				if (count > 2 && !((column + 1) == Columns) && !((row + 1) == Rows) && !(column == 0) && !(row == 0)) {
					tmp = Instantiate (Statue, new Vector3 (((x + CellWidth / 2) - 2.4f), 0, z) + Statue.transform.position, Quaternion.Euler (0, 90, 0)) as GameObject;// right
					tmp.transform.Rotate (270, 0, 0);
				} else {
					if (cell.WallRight) {
						if ((column + 1) == Columns) {
							tmp = Instantiate (Fence, new Vector3 (x + CellWidth / 2, 0, z) + Fence.transform.position, Quaternion.Euler (0, 90, 0)) as GameObject;// right
							tmp.transform.Rotate (0, 0, 0);
						} else {
							tmp = Instantiate (Wall, new Vector3 (x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler (0, 90, 0)) as GameObject;// right
							if (Random.value < 0.95) {
								
							} else {
								tmp = Instantiate (SpikeTrap, new Vector3 (x + CellWidth / 2, 0, z) + SpikeTrap.transform.position, Quaternion.Euler (0, 90, 0)) as GameObject;// right
								tmp.transform.Rotate (0, 90, 0);
							}
						}
						tmp.transform.parent = transform;
					}
					if (cell.WallFront) {
						if ((row + 1) == Rows) {
							if ((column + 1) == Columns) {
								tmp = Instantiate (Door, new Vector3 (x, 0, z + CellHeight / 2) + Door.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;// front

							} else {
								tmp = Instantiate (Fence, new Vector3 (x, 0, z + CellHeight / 2) + Fence.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;// front
								tmp.transform.Rotate (0, 0, 0);}
						} else {
							tmp = Instantiate (Wall, new Vector3 (x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;// front
							if (Random.value < 0.95) {
								
							} else {
								tmp = Instantiate (SpikeTrap, new Vector3 (x, 0, z + CellHeight / 2) + SpikeTrap.transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;// front
								tmp.transform.Rotate (0, 90, 0);
							}
						}
						tmp.transform.parent = transform;
					}
					if (cell.WallLeft) {
						if (column == 0) {
							tmp = Instantiate (Fence, new Vector3 (x - CellWidth / 2, 0, z) + Fence.transform.position, Quaternion.Euler (0, 270, 0)) as GameObject;// left
							tmp.transform.Rotate (0, 0, 0);
						} else {
							tmp = Instantiate (Wall, new Vector3 (x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler (0, 270, 0)) as GameObject;// left

							if (Random.value < 0.95) {
							} else {
								tmp = Instantiate (SpikeTrap, new Vector3 (x - CellWidth / 2, 0, z) + SpikeTrap.transform.position, Quaternion.Euler (0, 270, 0)) as GameObject;// left
								tmp.transform.Rotate (0, 90, 0);
							}
						}
						tmp.transform.parent = transform;
					}
					if (cell.WallBack) {
						if (row == 0) {
							tmp = Instantiate (Fence, new Vector3 (x, 0, z - CellHeight / 2) + Fence.transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;// back
							tmp.transform.Rotate (0, 0, 0);
						} else {
							tmp = Instantiate (Wall, new Vector3 (x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;// back

							if (Random.value < 0.95) {
							} else {
								tmp = Instantiate (SpikeTrap, new Vector3 (x, 0, z - CellHeight / 2) + SpikeTrap.transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;// back
								tmp.transform.Rotate (0, 90, 0);
							}
						}
						tmp.transform.parent = transform;
					}
					if (cell.IsGoal && GoalPrefab != null) {
						tmp = Instantiate (GoalPrefab, new Vector3 (x, 1, z), Quaternion.Euler (0, 0, 0)) as GameObject;
						tmp = Instantiate (GoalPrefab, new Vector3 (x, 3, z), Quaternion.Euler (0, 0, 0)) as GameObject;
						tmp = Instantiate (GoalPrefab, new Vector3 (x, 5, z), Quaternion.Euler (0, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
				}
			}
		}
		if(Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float tempVar = (AddGaps) ? .2f : 0;
					float x = column*(CellWidth+(tempVar));
					float z = row*(CellHeight+(tempVar));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent.Rotate (0, 90, 0);
					tmp.transform.parent = transform;
				}
			}
		}

		MazeCell[,] cells = mMazeGenerator.GetAllCells ();
		List<float[]> validPatrols = new List<float[]> ();
		for (int col = 0; col < cells.GetLength(0); col++) {
			for (int row = 0; row < cells.GetLength(1) - 2; row++) {
				if ((cells[row,col].WallLeft == false) && (cells[row+1,col].WallRight == false) && (cells[row+1,col].WallLeft == false) && (cells[row+2,col].WallRight == false)) {
					try {
						float xPos = row*CellHeight;
						float zPos = col*CellWidth;

						float[] patrolPair = {xPos, zPos, xPos + 2*CellHeight, zPos};
						validPatrols.Add(patrolPair);
					} catch {}
				}
			}
		}		
		for (int col = 0; col < cells.GetLength(0) - 2; col++) {
			for (int row = 0; row < cells.GetLength(1); row++) {
				if ((cells[row,col].WallBack == false) && (cells[row,col+1].WallFront == false) && (cells[row,col+1].WallBack == false) && (cells[row,col+1].WallFront == false)) {
					try {
						float xPos = row*CellHeight;
						float zPos = col*CellWidth;

						float[] patrolPair = {xPos, zPos, xPos, zPos + 2*CellWidth};
						validPatrols.Add(patrolPair);
					} catch {}
				}
			}
		}
		foreach (float[] patrolPair in validPatrols) {
			if (Random.value >= 0.0) {
				GameObject InitialPoint = new GameObject ();
				InitialPoint.transform.position = new Vector3 ((float)patrolPair.GetValue (0), 1, (float)patrolPair.GetValue (1));
				GameObject FinalPoint = new GameObject ();
				FinalPoint.transform.position = new Vector3 ((float)patrolPair.GetValue (2), 1, (float)patrolPair.GetValue (3));
				Transform[] patrolPoints = { InitialPoint.transform, FinalPoint.transform };

				GameObject enemy = Instantiate (PatrolEnemy) as GameObject;
				enemy.GetComponent<Patrol> ().patrolPoints = patrolPoints;
				enemy.GetComponent<Patrol> ().InitializeInfo ();
			}
		}
	}
}
