using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
namespace Freeria
{
	public class Main : Game
	{
		private const int MF_BYPOSITION = 1024;
		public const int sectionWidth = 200;
		public const int sectionHeight = 150;
		public const int maxTileSets = 107;
		public const int maxWallTypes = 21;
		public const int maxBackgrounds = 7;
		public const int maxDust = 1000;
		public const int maxCombatText = 100;
		public const int maxItemText = 100;
		public const int maxPlayers = 255;
		public const int maxChests = 1000;
		public const int maxItemTypes = 364;
		public const int maxItems = 200;
		public const int maxBuffs = 27;
		public const int maxProjectileTypes = 56;
		public const int maxProjectiles = 1000;
		public const int maxNPCTypes = 74;
		public const int maxNPCs = 1000;
		public const int maxGoreTypes = 99;
		public const int maxGore = 200;
		public const int maxInventory = 44;
		public const int maxItemSounds = 21;
		public const int maxNPCHitSounds = 3;
		public const int maxNPCKilledSounds = 5;
		public const int maxLiquidTypes = 2;
		public const int maxMusic = 9;
		public const int numArmorHead = 29;
		public const int numArmorBody = 17;
		public const int numArmorLegs = 16;
		public const double dayLength = 54000.0;
		public const double nightLength = 32400.0;
		public const int maxStars = 130;
		public const int maxStarTypes = 5;
		public const int maxClouds = 100;
		public const int maxCloudTypes = 4;
		public const int maxHair = 36;
		public static int curRelease = 22;
		public static string versionNumber = "v. Codename Lion";
		public static string versionNumber2 = "v. Codename Lion";
		public static bool skipMenu = false;
		public static bool verboseNetplay = false;
		public static bool stopTimeOuts = false;
		public static bool showSpam = false;
		public static bool showItemOwner = false;
		public static bool showSplash = true;
		public static bool ignoreErrors = true;
		public static string defaultIP = "";
		public static int maxScreenW = 1920;
		public static int minScreenW = 800;
		public static int maxScreenH = 1200;
		public static int minScreenH = 600;
		public static float iS = 1f;
		public static bool[] debuff = new bool[27];
		public static string[] buffName = new string[27];
		public static string[] buffTip = new string[27];
		public static int maxMP = 10;
		public static string[] recentWorld = new string[Main.maxMP];
		public static string[] recentIP = new string[Main.maxMP];
		public static int[] recentPort = new int[Main.maxMP];
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Process tServer = new Process();
		private static Stopwatch saveTime = new Stopwatch();
		public static MouseState mouseState = Mouse.GetState();
		public static MouseState oldMouseState = Mouse.GetState();
		public static KeyboardState keyState = Keyboard.GetState();
		public static Color mcColor = new Color(125, 125, 255);
		public static Color hcColor = new Color(200, 125, 255);
		public static bool mouseHC = false;
		public static string chestText = "Chest";
		public static bool craftingHide = false;
		public static bool armorHide = false;
		public static float craftingAlpha = 1f;
		public static float armorAlpha = 1f;
		public static float[] buffAlpha = new float[27];
		public static Item trashItem = new Item();
		public float chestLootScale = 1f;
		public bool chestLootHover;
		public float chestStackScale = 1f;
		public bool chestStackHover;
		public float chestDepositScale = 1f;
		public bool chestDepositHover;
		public static int teamCooldown = 0;
		public static int teamCooldownLen = 300;
		public static bool gamePaused = false;
		public static int updateTime = 0;
		public static int drawTime = 0;
		public static int frameRate = 0;
		public static bool frameRelease = false;
		public static bool showFrameRate = false;
		public static int magmaBGFrame = 0;
		public static int magmaBGFrameCounter = 0;
		public static int saveTimer = 0;
		public static bool autoJoin = false;
		public static bool serverStarting = false;
		public static float leftWorld = 0f;
		public static float rightWorld = 134400f;
		public static float topWorld = 0f;
		public static float bottomWorld = 38400f;
		public static int maxTilesX = (int)Main.rightWorld / 16 + 1;
		public static int maxTilesY = (int)Main.bottomWorld / 16 + 1;
		public static int maxSectionsX = Main.maxTilesX / 200;
		public static int maxSectionsY = Main.maxTilesY / 150;
		public static int numDust = 1000;
		public static int maxNetPlayers = 255;
		public static float caveParrallax = 1f;
		public static string[] tileName = new string[107];
		public static int dungeonX;
		public static int dungeonY;
		public static Liquid[] liquid = new Liquid[Liquid.resLiquid];
		public static LiquidBuffer[] liquidBuffer = new LiquidBuffer[10000];
		public static bool dedServ = false;
		public static int spamCount = 0;
		public int curMusic;
		public int newMusic;
		public static bool showItemText = true;
		public static bool autoSave = true;
		public static string buffString = "";
		public static string libPath = "";
		public static string statusText = "";
		public static string worldName = "";
		public static int worldID;
		public static int background = 0;
		public static Color tileColor;
		public static double worldSurface;
		public static double rockLayer;
		public static Color[] teamColor = new Color[5];
		public static bool dayTime = true;
		public static double time = 13500.0;
		public static int moonPhase = 0;
		public static short sunModY = 0;
		public static short moonModY = 0;
		public static bool grabSky = false;
		public static bool bloodMoon = false;
		public static int checkForSpawns = 0;
		public static int helpText = 0;
		public static bool autoGen = false;
		public static bool autoPause = false;
		public static int numStars;
		public static int cloudLimit = 100;
		public static int numClouds = Main.cloudLimit;
		public static float windSpeed = 0f;
		public static float windSpeedSpeed = 0f;
		public static Cloud[] cloud = new Cloud[100];
		public static bool resetClouds = true;
		public static int evilTiles;
		public static int meteorTiles;
		public static int jungleTiles;
		public static int dungeonTiles;
		public static int fadeCounter = 0;
		public static float invAlpha = 1f;
		public static float invDir = 1f;
		[ThreadStatic]
		public static Random rand;
		public static Texture2D[] armorHeadTexture = new Texture2D[29];
		public static Texture2D[] armorBodyTexture = new Texture2D[17];
		public static Texture2D[] armorArmTexture = new Texture2D[17];
		public static Texture2D[] armorLegTexture = new Texture2D[16];
		public static Texture2D trashTexture;
		public static Texture2D chainTexture;
		public static Texture2D chain2Texture;
		public static Texture2D chain3Texture;
		public static Texture2D chain4Texture;
		public static Texture2D chain5Texture;
		public static Texture2D chain6Texture;
		public static Texture2D cdTexture;
		public static Texture2D boneArmTexture;
		public static Texture2D[] HBLockTexture = new Texture2D[2];
		public static Texture2D[] buffTexture = new Texture2D[27];
		public static Texture2D[] itemTexture = new Texture2D[364];
		public static Texture2D[] npcTexture = new Texture2D[74];
		public static Texture2D[] projectileTexture = new Texture2D[56];
		public static Texture2D[] goreTexture = new Texture2D[99];
		public static Texture2D cursorTexture;
		public static Texture2D dustTexture;
		public static Texture2D sunTexture;
		public static Texture2D sun2Texture;
		public static Texture2D moonTexture;
		public static Texture2D[] tileTexture = new Texture2D[107];
		public static Texture2D blackTileTexture;
		public static Texture2D[] wallTexture = new Texture2D[21];
		public static Texture2D[] backgroundTexture = new Texture2D[7];
		public static Texture2D[] cloudTexture = new Texture2D[4];
		public static Texture2D[] starTexture = new Texture2D[5];
		public static Texture2D[] liquidTexture = new Texture2D[2];
		public static Texture2D heartTexture;
		public static Texture2D manaTexture;
		public static Texture2D bubbleTexture;
		public static Texture2D[] treeTopTexture = new Texture2D[3];
		public static Texture2D shroomCapTexture;
		public static Texture2D[] treeBranchTexture = new Texture2D[3];
		public static Texture2D inventoryBackTexture;
		public static Texture2D inventoryBack2Texture;
		public static Texture2D inventoryBack3Texture;
		public static Texture2D inventoryBack4Texture;
		public static Texture2D inventoryBack5Texture;
		public static Texture2D inventoryBack6Texture;
		public static Texture2D inventoryBack7Texture;
		public static Texture2D inventoryBack8Texture;
		public static Texture2D inventoryBack9Texture;
		public static Texture2D inventoryBack10Texture;
		public static Texture2D logoTexture;
		public static Texture2D textBackTexture;
		public static Texture2D chatTexture;
		public static Texture2D chat2Texture;
		public static Texture2D chatBackTexture;
		public static Texture2D teamTexture;
		public static Texture2D reTexture;
		public static Texture2D raTexture;
		public static Texture2D splashTexture;
		public static Texture2D fadeTexture;
		public static Texture2D ninjaTexture;
		public static Texture2D antLionTexture;
		public static Texture2D spikeBaseTexture;
		public static Texture2D ghostTexture;
		public static Texture2D playerEyeWhitesTexture;
		public static Texture2D playerEyesTexture;
		public static Texture2D playerHandsTexture;
		public static Texture2D playerHands2Texture;
		public static Texture2D playerHeadTexture;
		public static Texture2D playerPantsTexture;
		public static Texture2D playerShirtTexture;
		public static Texture2D playerShoesTexture;
		public static Texture2D playerBeltTexture;
		public static Texture2D playerUnderShirtTexture;
		public static Texture2D playerUnderShirt2Texture;
		public static Texture2D[] playerHairTexture = new Texture2D[36];
		public static SoundEffect[] soundDig = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceDig = new SoundEffectInstance[3];
		public static SoundEffect[] soundTink = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceTink = new SoundEffectInstance[3];
		public static SoundEffect[] soundPlayerHit = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstancePlayerHit = new SoundEffectInstance[3];
		public static SoundEffect[] soundFemaleHit = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceFemaleHit = new SoundEffectInstance[3];
		public static SoundEffect soundPlayerKilled;
		public static SoundEffectInstance soundInstancePlayerKilled;
		public static SoundEffect soundGrass;
		public static SoundEffectInstance soundInstanceGrass;
		public static SoundEffect soundGrab;
		public static SoundEffectInstance soundInstanceGrab;
		public static SoundEffect[] soundItem = new SoundEffect[22];
		public static SoundEffectInstance[] soundInstanceItem = new SoundEffectInstance[22];
		public static SoundEffect[] soundNPCHit = new SoundEffect[4];
		public static SoundEffectInstance[] soundInstanceNPCHit = new SoundEffectInstance[4];
		public static SoundEffect[] soundNPCKilled = new SoundEffect[6];
		public static SoundEffectInstance[] soundInstanceNPCKilled = new SoundEffectInstance[6];
		public static SoundEffect soundDoorOpen;
		public static SoundEffectInstance soundInstanceDoorOpen;
		public static SoundEffect soundDoorClosed;
		public static SoundEffectInstance soundInstanceDoorClosed;
		public static SoundEffect soundMenuOpen;
		public static SoundEffectInstance soundInstanceMenuOpen;
		public static SoundEffect soundMenuClose;
		public static SoundEffectInstance soundInstanceMenuClose;
		public static SoundEffect soundMenuTick;
		public static SoundEffectInstance soundInstanceMenuTick;
		public static SoundEffect soundShatter;
		public static SoundEffectInstance soundInstanceShatter;
		public static SoundEffect[] soundZombie = new SoundEffect[3];
		public static SoundEffectInstance[] soundInstanceZombie = new SoundEffectInstance[3];
		public static SoundEffect[] soundRoar = new SoundEffect[2];
		public static SoundEffectInstance[] soundInstanceRoar = new SoundEffectInstance[2];
		public static SoundEffect[] soundSplash = new SoundEffect[2];
		public static SoundEffectInstance[] soundInstanceSplash = new SoundEffectInstance[2];
		public static SoundEffect soundDoubleJump;
		public static SoundEffectInstance soundInstanceDoubleJump;
		public static SoundEffect soundRun;
		public static SoundEffectInstance soundInstanceRun;
		public static SoundEffect soundCoins;
		public static SoundEffectInstance soundInstanceCoins;
		public static SoundEffect soundUnlock;
		public static SoundEffectInstance soundInstanceUnlock;
		public static SoundEffect soundChat;
		public static SoundEffectInstance soundInstanceChat;
		public static SoundEffect soundMaxMana;
		public static SoundEffectInstance soundInstanceMaxMana;
		public static SoundEffect soundDrown;
		public static SoundEffectInstance soundInstanceDrown;
		public static AudioEngine engine;
		public static SoundBank soundBank;
		public static WaveBank waveBank;
		public static Cue[] music = new Cue[9];
		public static float[] musicFade = new float[9];
		public static float musicVolume = 0.75f;
		public static float soundVolume = 1f;
		public static SpriteFont fontItemStack;
		public static SpriteFont fontMouseText;
		public static SpriteFont fontDeathText;
		public static SpriteFont[] fontCombatText = new SpriteFont[2];
		public static bool[] tileMergeDirt = new bool[107];
		public static bool[] tileCut = new bool[107];
		public static bool[] tileAlch = new bool[107];
		public static int[] tileShine = new int[107];
		public static bool[] wallHouse = new bool[21];
		public static bool[] tileStone = new bool[107];
		public static bool[] tileWaterDeath = new bool[107];
		public static bool[] tileLavaDeath = new bool[107];
		public static bool[] tileTable = new bool[107];
		public static bool[] tileBlockLight = new bool[107];
		public static bool[] tileDungeon = new bool[107];
		public static bool[] tileSolidTop = new bool[107];
		public static bool[] tileSolid = new bool[107];
		public static bool[] tileNoAttach = new bool[107];
		public static bool[] tileNoFail = new bool[107];
		public static bool[] tileFrameImportant = new bool[107];
		public static int[] backgroundWidth = new int[7];
		public static int[] backgroundHeight = new int[7];
		public static bool tilesLoaded = false;
		public static Tile[,] tile = new Tile[Main.maxTilesX, Main.maxTilesY];
		public static Dust[] dust = new Dust[1001];
		public static Star[] star = new Star[130];
		public static Item[] item = new Item[201];
		public static NPC[] npc = new NPC[1001];
		public static Gore[] gore = new Gore[201];
		public static Projectile[] projectile = new Projectile[1001];
		public static CombatText[] combatText = new CombatText[100];
		public static ItemText[] itemText = new ItemText[100];
		public static Chest[] chest = new Chest[1000];
		public static Sign[] sign = new Sign[1000];
		public static Vector2 screenPosition;
		public static Vector2 screenLastPosition;
		public static int screenWidth = 800;
		public static int screenHeight = 600;
		public static int chatLength = 600;
		public static bool chatMode = false;
		public static bool chatRelease = false;
		public static int numChatLines = 7;
		public static string chatText = "";
		public static ChatLine[] chatLine = new ChatLine[Main.numChatLines];
		public static bool inputTextEnter = false;
		public static float[] hotbarScale = new float[]
		{
			1f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f, 
			0.75f
		};
		public static byte mouseTextColor = 0;
		public static int mouseTextColorChange = 1;
		public static bool mouseLeftRelease = false;
		public static bool mouseRightRelease = false;
		public static bool playerInventory = false;
		public static int stackSplit;
		public static int stackCounter = 0;
		public static int stackDelay = 7;
		public static Item mouseItem = new Item();
		public static Item guideItem = new Item();
		private static float inventoryScale = 0.75f;
		public static bool hasFocus = true;
		public static Recipe[] recipe = new Recipe[Recipe.maxRecipes];
		public static int[] availableRecipe = new int[Recipe.maxRecipes];
		public static float[] availableRecipeY = new float[Recipe.maxRecipes];
		public static int numAvailableRecipes;
		public static int focusRecipe;
		public static int myPlayer = 0;
		public static Player[] player = new Player[256];
		public static int spawnTileX;
		public static int spawnTileY;
		public static bool npcChatRelease = false;
		public static bool editSign = false;
		public static string signText = "";
		public static string npcChatText = "";
		public static bool npcChatFocus1 = false;
		public static bool npcChatFocus2 = false;
		public static bool npcChatFocus3 = false;
		public static int npcShop = 0;
		public Chest[] shop = new Chest[6];
		public static bool craftGuide = false;
		private static Item toolTip = new Item();
		private static int backSpaceCount = 0;
		public static string motd = "";
		public bool toggleFullscreen;
		private int numDisplayModes;
		private int[] displayWidth = new int[99];
		private int[] displayHeight = new int[99];
		public static bool gameMenu = true;
		public static Player[] loadPlayer = new Player[5];
		public static string[] loadPlayerPath = new string[5];
		private static int numLoadPlayers = 0;
		public static string playerPathName;
		public static string[] loadWorld = new string[999];
		public static string[] loadWorldPath = new string[999];
		private static int numLoadWorlds = 0;
		public static string worldPathName;
		public static string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Freeria";
		public static string WorldPath = Main.SavePath + "\\Worlds";
		public static string PlayerPath = Main.SavePath + "\\Players";
		public static string[] itemName = new string[364];
		private static KeyboardState inputText;
		private static KeyboardState oldInputText;
		public static int invasionType = 0;
		public static double invasionX = 0.0;
		public static int invasionSize = 0;
		public static int invasionDelay = 0;
		public static int invasionWarn = 0;
		public static int[] npcFrameCount = new int[]
		{
			1, 
			2, 
			2, 
			3, 
			6, 
			2, 
			2, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			1, 
			2, 
			16, 
			14, 
			16, 
			14, 
			15, 
			16, 
			2, 
			10, 
			1, 
			16, 
			16, 
			16, 
			3, 
			1, 
			15, 
			3, 
			1, 
			3, 
			1, 
			1, 
			16, 
			16, 
			1, 
			1, 
			1, 
			3, 
			3, 
			15, 
			3, 
			7, 
			7, 
			4, 
			5, 
			5, 
			5, 
			3, 
			3, 
			16, 
			6, 
			3, 
			6, 
			6, 
			2, 
			5, 
			3, 
			2, 
			7, 
			7, 
			4, 
			2, 
			8, 
			1, 
			5, 
			1, 
			2, 
			4, 
			16
		};
		private static bool mouseExit = false;
		private static float exitScale = 0.8f;
		public static Player clientPlayer = new Player();
		public static string getIP = Main.defaultIP;
		public static string getPort = Convert.ToString(Netplay.serverPort);
		public static bool menuMultiplayer = false;
		public static bool menuServer = false;
		public static int netMode = 0;
		public static int timeOut = 120;
		public static int netPlayCounter;
		public static int lastNPCUpdate;
		public static int lastItemUpdate;
		public static int maxNPCUpdates = 15;
		public static int maxItemUpdates = 10;
		public static string cUp = "W";
		public static string cLeft = "A";
		public static string cDown = "S";
		public static string cRight = "D";
		public static string cJump = "Space";
		public static string cThrowItem = "Q";
		public static string cInv = "Escape";
		public static string cHeal = "H";
		public static string cMana = "M";
		public static string cBuff = "B";
		public static string cHook = "E";
		public static Color mouseColor = new Color(255, 50, 95);
		public static Color cursorColor = Color.White;
		public static int cursorColorDirection = 1;
		public static float cursorAlpha = 0f;
		public static float cursorScale = 0f;
		public static bool signBubble = false;
		public static int signX = 0;
		public static int signY = 0;
		public static bool hideUI = false;
		public static bool releaseUI = false;
		public static bool fixedTiming = false;
		private int splashCounter;
		public static string oldStatusText = "";
		public static bool autoShutdown = false;
		private float logoRotation;
		private float logoRotationDirection = 1f;
		private float logoRotationSpeed = 1f;
		private float logoScale = 1f;
		private float logoScaleDirection = 1f;
		private float logoScaleSpeed = 1f;
		private static int maxMenuItems = 14;
		private float[] menuItemScale = new float[Main.maxMenuItems];
		private int focusMenu = -1;
		private int selectedMenu = -1;
		private int selectedMenu2 = -1;
		private int selectedPlayer;
		private int selectedWorld;
		public static int menuMode = 0;
		private int textBlinkerCount;
		private int textBlinkerState;
		public static string newWorldName = "";
		private static int accSlotCount = 0;
		private Color selColor = Color.White;
		private int focusColor;
		private int colorDelay;
		private int setKey = -1;
		private int bgScroll;
		public static bool autoPass = false;
		public static int menuFocus = 0;
		[DllImport("User32")]
		private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
		[DllImport("User32")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
		[DllImport("User32")]
		private static extern int GetMenuItemCount(IntPtr hWnd);
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);
		public static void LoadWorlds()
		{
			Directory.CreateDirectory(Main.WorldPath);
			string[] files = Directory.GetFiles(Main.WorldPath, "*.wld");
			int num = files.Length;
			if (!Main.dedServ && num > 5)
			{
				num = 5;
			}
			for (int i = 0; i < num; i++)
			{
				Main.loadWorldPath[i] = files[i];
				try
				{
					using (FileStream fileStream = new FileStream(Main.loadWorldPath[i], FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							binaryReader.ReadInt32();
							Main.loadWorld[i] = binaryReader.ReadString();
							binaryReader.Close();
						}
					}
				}
				catch
				{
					Main.loadWorld[i] = Main.loadWorldPath[i];
				}
			}
			Main.numLoadWorlds = num;
		}
		private static void LoadPlayers()
		{
			Directory.CreateDirectory(Main.PlayerPath);
			string[] files = Directory.GetFiles(Main.PlayerPath, "*.plr");
			int num = files.Length;
			if (num > 5)
			{
				num = 5;
			}
			for (int i = 0; i < 5; i++)
			{
				Main.loadPlayer[i] = new Player();
				if (i < num)
				{
					Main.loadPlayerPath[i] = files[i];
					Main.loadPlayer[i] = Player.LoadPlayer(Main.loadPlayerPath[i]);
				}
			}
			Main.numLoadPlayers = num;
		}
		protected void OpenRecent()
		{
			try
			{
				if (File.Exists(Main.SavePath + "\\servers.dat"))
				{
					using (FileStream fileStream = new FileStream(Main.SavePath + "\\servers.dat", FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							binaryReader.ReadInt32();
							for (int i = 0; i < 10; i++)
							{
								Main.recentWorld[i] = binaryReader.ReadString();
								Main.recentIP[i] = binaryReader.ReadString();
								Main.recentPort[i] = binaryReader.ReadInt32();
							}
						}
					}
				}
			}
			catch
			{
			}
		}
		public static void SaveRecent()
		{
			Directory.CreateDirectory(Main.SavePath);
			try
			{
				File.SetAttributes(Main.SavePath + "\\servers.dat", FileAttributes.Normal);
			}
			catch
			{
			}
			try
			{
				using (FileStream fileStream = new FileStream(Main.SavePath + "\\servers.dat", FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(Main.curRelease);
						for (int i = 0; i < 10; i++)
						{
							binaryWriter.Write(Main.recentWorld[i]);
							binaryWriter.Write(Main.recentIP[i]);
							binaryWriter.Write(Main.recentPort[i]);
						}
					}
				}
			}
			catch
			{
			}
		}
		protected void SaveSettings()
		{
			Directory.CreateDirectory(Main.SavePath);
			try
			{
				File.SetAttributes(Main.SavePath + "\\config.dat", FileAttributes.Normal);
			}
			catch
			{
			}
			try
			{
				using (FileStream fileStream = new FileStream(Main.SavePath + "\\config.dat", FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(Main.curRelease);
						binaryWriter.Write(this.graphics.IsFullScreen);
						binaryWriter.Write(Main.mouseColor.R);
						binaryWriter.Write(Main.mouseColor.G);
						binaryWriter.Write(Main.mouseColor.B);
						binaryWriter.Write(Main.soundVolume);
						binaryWriter.Write(Main.musicVolume);
						binaryWriter.Write(Main.cUp);
						binaryWriter.Write(Main.cDown);
						binaryWriter.Write(Main.cLeft);
						binaryWriter.Write(Main.cRight);
						binaryWriter.Write(Main.cJump);
						binaryWriter.Write(Main.cThrowItem);
						binaryWriter.Write(Main.cInv);
						binaryWriter.Write(Main.cHeal);
						binaryWriter.Write(Main.cMana);
						binaryWriter.Write(Main.cBuff);
						binaryWriter.Write(Main.cHook);
						binaryWriter.Write(Main.caveParrallax);
						binaryWriter.Write(Main.fixedTiming);
						binaryWriter.Write(this.graphics.PreferredBackBufferWidth);
						binaryWriter.Write(this.graphics.PreferredBackBufferHeight);
						binaryWriter.Write(Main.autoSave);
						binaryWriter.Write(Main.autoPause);
						binaryWriter.Write(Main.showItemText);
						binaryWriter.Close();
					}
				}
			}
			catch
			{
			}
		}
		protected void OpenSettings()
		{
			try
			{
				if (File.Exists(Main.SavePath + "\\config.dat"))
				{
					using (FileStream fileStream = new FileStream(Main.SavePath + "\\config.dat", FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							int num = binaryReader.ReadInt32();
							bool flag = binaryReader.ReadBoolean();
							Main.mouseColor.R = binaryReader.ReadByte();
							Main.mouseColor.G = binaryReader.ReadByte();
							Main.mouseColor.B = binaryReader.ReadByte();
							Main.soundVolume = binaryReader.ReadSingle();
							Main.musicVolume = binaryReader.ReadSingle();
							Main.cUp = binaryReader.ReadString();
							Main.cDown = binaryReader.ReadString();
							Main.cLeft = binaryReader.ReadString();
							Main.cRight = binaryReader.ReadString();
							Main.cJump = binaryReader.ReadString();
							Main.cThrowItem = binaryReader.ReadString();
							if (num >= 1)
							{
								Main.cInv = binaryReader.ReadString();
							}
							if (num >= 12)
							{
								Main.cHeal = binaryReader.ReadString();
								Main.cMana = binaryReader.ReadString();
								Main.cBuff = binaryReader.ReadString();
							}
							if (num >= 13)
							{
								Main.cHook = binaryReader.ReadString();
							}
							Main.caveParrallax = binaryReader.ReadSingle();
							if (num >= 2)
							{
								Main.fixedTiming = binaryReader.ReadBoolean();
							}
							if (num >= 4)
							{
								this.graphics.PreferredBackBufferWidth = binaryReader.ReadInt32();
								this.graphics.PreferredBackBufferHeight = binaryReader.ReadInt32();
							}
							if (num >= 8)
							{
								Main.autoSave = binaryReader.ReadBoolean();
							}
							if (num >= 9)
							{
								Main.autoPause = binaryReader.ReadBoolean();
							}
							if (num >= 19)
							{
								Main.showItemText = binaryReader.ReadBoolean();
							}
							binaryReader.Close();
							if (flag && !this.graphics.IsFullScreen)
							{
								this.graphics.ToggleFullScreen();
							}
						}
					}
				}
			}
			catch
			{
			}
		}
		private static void ErasePlayer(int i)
		{
			try
			{
				File.Delete(Main.loadPlayerPath[i]);
				File.Delete(Main.loadPlayerPath[i] + ".bak");
				Main.LoadPlayers();
			}
			catch
			{
			}
		}
		private static void EraseWorld(int i)
		{
			try
			{
				File.Delete(Main.loadWorldPath[i]);
				File.Delete(Main.loadWorldPath[i] + ".bak");
				Main.LoadWorlds();
			}
			catch
			{
			}
		}
		private static string nextLoadPlayer()
		{
			int num = 1;
			while (File.Exists(string.Concat(new object[]
			{
				Main.PlayerPath, 
				"\\player", 
				num, 
				".plr"
			})))
			{
				num++;
			}
			return string.Concat(new object[]
			{
				Main.PlayerPath, 
				"\\player", 
				num, 
				".plr"
			});
		}
		private static string nextLoadWorld()
		{
			int num = 1;
			while (File.Exists(string.Concat(new object[]
			{
				Main.WorldPath, 
				"\\world", 
				num, 
				".wld"
			})))
			{
				num++;
			}
			return string.Concat(new object[]
			{
				Main.WorldPath, 
				"\\world", 
				num, 
				".wld"
			});
		}
		public void autoCreate(string newOpt)
		{
			if (newOpt == "0")
			{
				Main.autoGen = false;
				return;
			}
			if (newOpt == "1")
			{
				Main.maxTilesX = 4200;
				Main.maxTilesY = 1200;
				Main.autoGen = true;
				return;
			}
			if (newOpt == "2")
			{
				Main.maxTilesX = 6300;
				Main.maxTilesY = 1800;
				Main.autoGen = true;
				return;
			}
			if (newOpt == "3")
			{
				Main.maxTilesX = 8400;
				Main.maxTilesY = 2400;
				Main.autoGen = true;
			}
		}
		public void NewMOTD(string newMOTD)
		{
			Main.motd = newMOTD;
		}
		public void LoadDedConfig(string configPath)
		{
			if (File.Exists(configPath))
			{
				using (StreamReader streamReader = new StreamReader(configPath))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						try
						{
							if (text.Length > 6 && text.Substring(0, 6).ToLower() == "world=")
							{
								string text2 = text.Substring(6);
								Main.worldPathName = text2;
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "port=")
							{
								string value = text.Substring(5);
								try
								{
									int serverPort = Convert.ToInt32(value);
									Netplay.serverPort = serverPort;
								}
								catch
								{
								}
							}
							if (text.Length > 11 && text.Substring(0, 11).ToLower() == "maxplayers=")
							{
								string value2 = text.Substring(11);
								try
								{
									int num = Convert.ToInt32(value2);
									Main.maxNetPlayers = num;
								}
								catch
								{
								}
							}
							if (text.Length > 9 && text.Substring(0, 9).ToLower() == "password=")
							{
								string password = text.Substring(9);
								Netplay.password = password;
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "motd=")
							{
								string text3 = text.Substring(5);
								Main.motd = text3;
							}
							if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldpath=")
							{
								string worldPath = text.Substring(10);
								Main.WorldPath = worldPath;
							}
							if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldname=")
							{
								string text4 = text.Substring(10);
								Main.worldName = text4;
							}
							if (text.Length > 8 && text.Substring(0, 8).ToLower() == "banlist=")
							{
								string banFile = text.Substring(8);
								Netplay.banFile = banFile;
							}
							if (text.Length > 11 && text.Substring(0, 11).ToLower() == "autocreate=")
							{
								string a = text.Substring(11);
								if (a == "0")
								{
									Main.autoGen = false;
								}
								else
								{
									if (a == "1")
									{
										Main.maxTilesX = 4200;
										Main.maxTilesY = 1200;
										Main.autoGen = true;
									}
									else
									{
										if (a == "2")
										{
											Main.maxTilesX = 6300;
											Main.maxTilesY = 1800;
											Main.autoGen = true;
										}
										else
										{
											if (a == "3")
											{
												Main.maxTilesX = 8400;
												Main.maxTilesY = 2400;
												Main.autoGen = true;
											}
										}
									}
								}
							}
							if (text.Length > 7 && text.Substring(0, 7).ToLower() == "secure=")
							{
								string a2 = text.Substring(7);
								if (a2 == "1")
								{
									Netplay.spamCheck = true;
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}
		public void SetNetPlayers(int mPlayers)
		{
			Main.maxNetPlayers = mPlayers;
		}
		public void SetWorld(string wrold)
		{
			Main.worldPathName = wrold;
		}
		public void SetWorldName(string wrold)
		{
			Main.worldName = wrold;
		}
		public void autoShut()
		{
			Main.autoShutdown = true;
		}
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		public void AutoPass()
		{
			Main.autoPass = true;
		}
		public void AutoJoin(string IP)
		{
			Main.defaultIP = IP;
			Main.getIP = IP;
			Netplay.SetIP(Main.defaultIP);
			Main.autoJoin = true;
		}
		public void AutoHost()
		{
			Main.menuMultiplayer = true;
			Main.menuServer = true;
			Main.menuMode = 1;
		}
		public void loadLib(string path)
		{
			Main.libPath = path;
			Main.LoadLibrary(Main.libPath);
		}
		public void DedServ()
		{
			Main.rand = new Random();
			if (Main.autoShutdown)
			{
				string text = "Freeria" + Main.rand.Next(2147483647);
				Console.Title = text;
				IntPtr intPtr = Main.FindWindow(null, text);
				if (intPtr != IntPtr.Zero)
				{
					Main.ShowWindow(intPtr, 0);
				}
			}
			else
			{
				Console.Title = "Freeria Server " + Main.versionNumber2;
			}
			Main.dedServ = true;
			Main.showSplash = false;
			this.Initialize();
			while (Main.worldPathName == null || Main.worldPathName == "")
			{
				Main.LoadWorlds();
				bool flag = true;
				while (flag)
				{
					Console.WriteLine("Freeria Server " + Main.versionNumber2);
					Console.WriteLine("");
					for (int i = 0; i < Main.numLoadWorlds; i++)
					{
						Console.WriteLine(string.Concat(new object[]
						{
							i + 1, 
							'\t', 
							'\t', 
							Main.loadWorld[i]
						}));
					}
					Console.WriteLine(string.Concat(new object[]
					{
						"n", 
						'\t', 
						'\t', 
						"New World"
					}));
					Console.WriteLine("d <number>" + '\t' + "Delete World");
					Console.WriteLine("");
					Console.Write("Choose World: ");
					string text2 = Console.ReadLine();
					try
					{
						Console.Clear();
					}
					catch
					{
					}
					if (text2.Length >= 2 && text2.Substring(0, 2).ToLower() == "d ")
					{
						try
						{
							int num = Convert.ToInt32(text2.Substring(2)) - 1;
							if (num < Main.numLoadWorlds)
							{
								Console.WriteLine("Freeria Server " + Main.versionNumber2);
								Console.WriteLine("");
								Console.WriteLine("Really delete " + Main.loadWorld[num] + "?");
								Console.Write("(y/n): ");
								string text3 = Console.ReadLine();
								if (text3.ToLower() == "y")
								{
									Main.EraseWorld(num);
								}
							}
						}
						catch
						{
						}
						try
						{
							Console.Clear();
							continue;
						}
						catch
						{
							continue;
						}
					}
					if (text2 == "n" || text2 == "N")
					{
						bool flag2 = true;
						while (flag2)
						{
							Console.WriteLine("Freeria Server " + Main.versionNumber2);
							Console.WriteLine("");
							Console.WriteLine("1" + '\t' + "Small");
							Console.WriteLine("2" + '\t' + "Medium");
							Console.WriteLine("3" + '\t' + "Large");
							Console.WriteLine("");
							Console.Write("Choose size: ");
							string value = Console.ReadLine();
							try
							{
								int num2 = Convert.ToInt32(value);
								if (num2 == 1)
								{
									Main.maxTilesX = 4200;
									Main.maxTilesY = 1200;
									flag2 = false;
								}
								else
								{
									if (num2 == 2)
									{
										Main.maxTilesX = 6300;
										Main.maxTilesY = 1800;
										flag2 = false;
									}
									else
									{
										if (num2 == 3)
										{
											Main.maxTilesX = 8400;
											Main.maxTilesY = 2400;
											flag2 = false;
										}
									}
								}
							}
							catch
							{
							}
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
						flag2 = true;
						while (flag2)
						{
							Console.WriteLine("Freeria Server " + Main.versionNumber2);
							Console.WriteLine("");
							Console.Write("Enter world name: ");
							Main.newWorldName = Console.ReadLine();
							if (Main.newWorldName != "" && Main.newWorldName != " " && Main.newWorldName != null)
							{
								flag2 = false;
							}
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
						Main.worldName = Main.newWorldName;
						Main.worldPathName = Main.nextLoadWorld();
						Main.menuMode = 10;
						WorldGen.CreateNewWorld();
						flag2 = false;
						while (Main.menuMode == 10)
						{
							if (Main.oldStatusText != Main.statusText)
							{
								Main.oldStatusText = Main.statusText;
								Console.WriteLine(Main.statusText);
							}
						}
						try
						{
							Console.Clear();
							continue;
						}
						catch
						{
							continue;
						}
					}
					try
					{
						int num3 = Convert.ToInt32(text2);
						num3--;
						if (num3 >= 0 && num3 < Main.numLoadWorlds)
						{
							bool flag3 = true;
							while (flag3)
							{
								Console.WriteLine("Freeria Server " + Main.versionNumber2);
								Console.WriteLine("");
								Console.Write("Max players (press enter for 8): ");
								string text4 = Console.ReadLine();
								try
								{
									if (text4 == "")
									{
										text4 = "8";
									}
									int num4 = Convert.ToInt32(text4);
									if (num4 <= 255 && num4 >= 1)
									{
										Main.maxNetPlayers = num4;
										flag3 = false;
									}
									flag3 = false;
								}
								catch
								{
								}
								try
								{
									Console.Clear();
								}
								catch
								{
								}
							}
							flag3 = true;
							while (flag3)
							{
								Console.WriteLine("Freeria Server " + Main.versionNumber2);
								Console.WriteLine("");
								Console.Write("Server port (press enter for 7777): ");
								string text5 = Console.ReadLine();
								try
								{
									if (text5 == "")
									{
										text5 = "7777";
									}
									int num5 = Convert.ToInt32(text5);
									if (num5 <= 65535)
									{
										Netplay.serverPort = num5;
										flag3 = false;
									}
								}
								catch
								{
								}
								try
								{
									Console.Clear();
								}
								catch
								{
								}
							}
							Console.WriteLine("Freeria Server " + Main.versionNumber2);
							Console.WriteLine("");
							Console.Write("Server password (press enter for none): ");
							Netplay.password = Console.ReadLine();
							Main.worldPathName = Main.loadWorldPath[num3];
							flag = false;
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
					}
					catch
					{
					}
				}
			}
			try
			{
				Console.Clear();
			}
			catch
			{
			}
			WorldGen.serverLoadWorld();
			Console.WriteLine("Freeria Server " + Main.versionNumber);
			Console.WriteLine("");
			while (!Netplay.ServerUp)
			{
				if (Main.oldStatusText != Main.statusText)
				{
					Main.oldStatusText = Main.statusText;
					Console.WriteLine(Main.statusText);
				}
			}
			try
			{
				Console.Clear();
			}
			catch
			{
			}
			Console.WriteLine("Freeria Server " + Main.versionNumber);
			Console.WriteLine("");
			Console.WriteLine("Listening on port " + Netplay.serverPort);
			Console.WriteLine("Type 'help' for a list of commands.");
			Console.WriteLine("");
			Console.Title = "Freeria Server: " + Main.worldName;
			Stopwatch stopwatch = new Stopwatch();
			if (!Main.autoShutdown)
			{
				Main.startDedInput();
			}
			stopwatch.Start();
			double num6 = 16.666666666666668;
			double num7 = 0.0;
			while (!Netplay.disconnect)
			{
				double num8 = (double)stopwatch.ElapsedMilliseconds;
				if (num8 + num7 >= num6)
				{
					num7 += num8 - num6;
					stopwatch.Reset();
					stopwatch.Start();
					if (Main.oldStatusText != Main.statusText)
					{
						Main.oldStatusText = Main.statusText;
						Console.WriteLine(Main.statusText);
					}
					if (num7 > 1000.0)
					{
						num7 = 1000.0;
					}
					if (Netplay.anyClients)
					{
						this.Update(new GameTime());
					}
					double num9 = (double)stopwatch.ElapsedMilliseconds + num7;
					if (num9 < num6)
					{
						int num10 = (int)(num6 - num9) - 1;
						if (num10 > 1)
						{
							Thread.Sleep(num10);
							if (!Netplay.anyClients)
							{
								num7 = 0.0;
								Thread.Sleep(10);
							}
						}
					}
				}
				Thread.Sleep(0);
			}
		}
		public static void startDedInput()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(Main.startDedInputCallBack), 1);
		}
		public static void startDedInputCallBack(object threadContext)
		{
			while (!Netplay.disconnect)
			{
				Console.Write(": ");
				string text = Console.ReadLine();
				string text2 = text;
				text = text.ToLower();
				try
				{
					if (text == "help")
					{
						Console.WriteLine("Available commands:");
						Console.WriteLine("");
						Console.WriteLine(string.Concat(new object[]
						{
							"help ", 
							'\t', 
							'\t', 
							" Displays a list of commands."
						}));
						Console.WriteLine("playing " + '\t' + " Shows the list of players");
						Console.WriteLine(string.Concat(new object[]
						{
							"clear ", 
							'\t', 
							'\t', 
							" Clear the console window."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"exit ", 
							'\t', 
							'\t', 
							" Shutdown the server and save."
						}));
						Console.WriteLine("exit-nosave " + '\t' + " Shutdown the server without saving.");
						Console.WriteLine(string.Concat(new object[]
						{
							"save ", 
							'\t', 
							'\t', 
							" Save the game world."
						}));
						Console.WriteLine("kick <player> " + '\t' + " Kicks a player from the server.");
						Console.WriteLine("ban <player> " + '\t' + " Bans a player from the server.");
						Console.WriteLine("password" + '\t' + " Show password.");
						Console.WriteLine("password <pass>" + '\t' + " Change password.");
						Console.WriteLine(string.Concat(new object[]
						{
							"version", 
							'\t', 
							'\t', 
							" Print version number."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"time", 
							'\t', 
							'\t', 
							" Display game time."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"port", 
							'\t', 
							'\t', 
							" Print the listening port."
						}));
						Console.WriteLine("maxplayers" + '\t' + " Print the max number of players.");
						Console.WriteLine("say <words>" + '\t' + " Send a message.");
						Console.WriteLine(string.Concat(new object[]
						{
							"motd", 
							'\t', 
							'\t', 
							" Print MOTD."
						}));
						Console.WriteLine("motd <words>" + '\t' + " Change MOTD.");
						Console.WriteLine(string.Concat(new object[]
						{
							"dawn", 
							'\t', 
							'\t', 
							" Change time to dawn."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"noon", 
							'\t', 
							'\t', 
							" Change time to noon."
						}));
						Console.WriteLine(string.Concat(new object[]
						{
							"dusk", 
							'\t', 
							'\t', 
							" Change time to dusk."
						}));
						Console.WriteLine("midnight" + '\t' + " Change time to midnight.");
						Console.WriteLine(string.Concat(new object[]
						{
							"settle", 
							'\t', 
							'\t', 
							" Settle all water."
						}));
					}
					else
					{
						if (text == "settle")
						{
							if (!Liquid.panicMode)
							{
								Liquid.StartPanic();
							}
							else
							{
								Console.WriteLine("Water is already settling");
							}
						}
						else
						{
							if (text == "dawn")
							{
								Main.dayTime = true;
								Main.time = 0.0;
								NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
							}
							else
							{
								if (text == "dusk")
								{
									Main.dayTime = false;
									Main.time = 0.0;
									NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
								}
								else
								{
									if (text == "noon")
									{
										Main.dayTime = true;
										Main.time = 27000.0;
										NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
									}
									else
									{
										if (text == "midnight")
										{
											Main.dayTime = false;
											Main.time = 16200.0;
											NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
										}
										else
										{
											if (text == "exit-nosave")
											{
												Netplay.disconnect = true;
											}
											else
											{
												if (text == "exit")
												{
													WorldGen.saveWorld(false);
													Netplay.disconnect = true;
												}
												else
												{
													if (text == "save")
													{
														WorldGen.saveWorld(false);
													}
													else
													{
														if (text == "time")
														{
															string text3 = "AM";
															double num = Main.time;
															if (!Main.dayTime)
															{
																num += 54000.0;
															}
															num = num / 86400.0 * 24.0;
															double num2 = 7.5;
															num = num - num2 - 12.0;
															if (num < 0.0)
															{
																num += 24.0;
															}
															if (num >= 12.0)
															{
																text3 = "PM";
															}
															int num3 = (int)num;
															double num4 = num - (double)num3;
															num4 = (double)((int)(num4 * 60.0));
															string text4 = string.Concat(num4);
															if (num4 < 10.0)
															{
																text4 = "0" + text4;
															}
															if (num3 > 12)
															{
																num3 -= 12;
															}
															if (num3 == 0)
															{
																num3 = 12;
															}
															Console.WriteLine(string.Concat(new object[]
															{
																"Time: ", 
																num3, 
																":", 
																text4, 
																" ", 
																text3
															}));
														}
														else
														{
															if (text == "maxplayers")
															{
																Console.WriteLine("Player limit: " + Main.maxNetPlayers);
															}
															else
															{
																if (text == "port")
																{
																	Console.WriteLine("Port: " + Netplay.serverPort);
																}
																else
																{
																	if (text == "version")
																	{
																		Console.WriteLine("Freeria Server " + Main.versionNumber);
																	}
																	else
																	{
																		if (text == "clear")
																		{
																			try
																			{
																				Console.Clear();
																				continue;
																			}
																			catch
																			{
																				continue;
																			}
																		}
																		if (text == "playing")
																		{
																			int num5 = 0;
																			for (int i = 0; i < 255; i++)
																			{
																				if (Main.player[i].active)
																				{
																					num5++;
																					Console.WriteLine(string.Concat(new object[]
																					{
																						Main.player[i].name, 
																						" (", 
																						Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint, 
																						")"
																					}));
																				}
																			}
																			if (num5 == 0)
																			{
																				Console.WriteLine("No players connected.");
																			}
																			else
																			{
																				if (num5 == 1)
																				{
																					Console.WriteLine("1 player connected.");
																				}
																				else
																				{
																					Console.WriteLine(num5 + " players connected.");
																				}
																			}
																		}
																		else
																		{
																			if (!(text == ""))
																			{
																				if (text == "motd")
																				{
																					if (Main.motd == "")
																					{
																						Console.WriteLine("Welcome to " + Main.worldName + "!");
																					}
																					else
																					{
																						Console.WriteLine("MOTD: " + Main.motd);
																					}
																				}
																				else
																				{
																					if (text.Length >= 5 && text.Substring(0, 5) == "motd ")
																					{
																						string text5 = text2.Substring(5);
																						Main.motd = text5;
																					}
																					else
																					{
																						if (text.Length == 8 && text.Substring(0, 8) == "password")
																						{
																							if (Netplay.password == "")
																							{
																								Console.WriteLine("No password set.");
																							}
																							else
																							{
																								Console.WriteLine("Password: " + Netplay.password);
																							}
																						}
																						else
																						{
																							if (text.Length >= 9 && text.Substring(0, 9) == "password ")
																							{
																								string text6 = text2.Substring(9);
																								if (text6 == "")
																								{
																									Netplay.password = "";
																									Console.WriteLine("Password disabled.");
																								}
																								else
																								{
																									Netplay.password = text6;
																									Console.WriteLine("Password: " + Netplay.password);
																								}
																							}
																							else
																							{
																								if (text == "say")
																								{
																									Console.WriteLine("Usage: say <words>");
																								}
																								else
																								{
																									if (text.Length >= 4 && text.Substring(0, 4) == "say ")
																									{
																										string text7 = text2.Substring(4);
																										if (text7 == "")
																										{
																											Console.WriteLine("Usage: say <words>");
																										}
																										else
																										{
																											Console.WriteLine("<Server> " + text7);
																											NetMessage.SendData(25, -1, -1, "<Server> " + text7, 255, 255f, 240f, 20f, 0);
																										}
																									}
																									else
																									{
																										if (text.Length == 4 && text.Substring(0, 4) == "kick")
																										{
																											Console.WriteLine("Usage: kick <player>");
																										}
																										else
																										{
																											if (text.Length >= 5 && text.Substring(0, 5) == "kick ")
																											{
																												string text8 = text.Substring(5);
																												text8 = text8.ToLower();
																												if (text8 == "")
																												{
																													Console.WriteLine("Usage: kick <player>");
																												}
																												else
																												{
																													for (int j = 0; j < 255; j++)
																													{
																														if (Main.player[j].active && Main.player[j].name.ToLower() == text8)
																														{
																															NetMessage.SendData(2, j, -1, "Kicked from server.", 0, 0f, 0f, 0f, 0);
																														}
																													}
																												}
																											}
																											else
																											{
																												if (text.Length == 3 && text.Substring(0, 3) == "ban")
																												{
																													Console.WriteLine("Usage: ban <player>");
																												}
																												else
																												{
																													if (text.Length >= 4 && text.Substring(0, 4) == "ban ")
																													{
																														string text9 = text.Substring(4);
																														text9 = text9.ToLower();
																														if (text9 == "")
																														{
																															Console.WriteLine("Usage: ban <player>");
																														}
																														else
																														{
																															for (int k = 0; k < 255; k++)
																															{
																																if (Main.player[k].active && Main.player[k].name.ToLower() == text9)
																																{
																																	Netplay.AddBan(k);
																																	NetMessage.SendData(2, k, -1, "Banned from server.", 0, 0f, 0f, 0f, 0);
																																}
																															}
																														}
																													}
																													else
																													{
																														Console.WriteLine("Invalid command.");
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch
				{
					Console.WriteLine("Invalid command.");
				}
			}
		}
		public Main()
		{
			this.graphics = new GraphicsDeviceManager(this);
			base.Content.RootDirectory = "Content";
		}
		protected override void Initialize()
		{
			Main.debuff[20] = true;
			Main.debuff[21] = true;
			Main.debuff[22] = true;
			Main.debuff[23] = true;
			Main.debuff[24] = true;
			Main.debuff[25] = true;
			Main.buffName[1] = "Obsidian Skin";
			Main.buffTip[1] = "Immune to lava";
			Main.buffName[2] = "Regeneration";
			Main.buffTip[2] = "Provides life regeneration";
			Main.buffName[3] = "Swiftness";
			Main.buffTip[3] = "%25 increased movement speed";
			Main.buffName[4] = "Gills";
			Main.buffTip[4] = "Breathe water instead of air";
			Main.buffName[5] = "Ironskin";
			Main.buffTip[5] = "Increase defense by 8";
			Main.buffName[6] = "Mana Regeneration";
			Main.buffTip[6] = "Increased mana regeneration";
			Main.buffName[7] = "Magic Power";
			Main.buffTip[7] = "20% increased magic damage";
			Main.buffName[8] = "Featherfall";
			Main.buffTip[8] = "Press UP or DOWN to control speed of descent";
			Main.buffName[9] = "Spelunker";
			Main.buffTip[9] = "Shows the location of treasure and ore";
			Main.buffName[10] = "Invisibility";
			Main.buffTip[10] = "Grants invisibility";
			Main.buffName[11] = "Shine";
			Main.buffTip[11] = "Emitting light";
			Main.buffName[12] = "Night Owl";
			Main.buffTip[12] = "Increased night vision";
			Main.buffName[13] = "Battle";
			Main.buffTip[13] = "Increased enemy spawn rate";
			Main.buffName[14] = "Thorns";
			Main.buffTip[14] = "Attackers also take damage";
			Main.buffName[15] = "Water Walking";
			Main.buffTip[15] = "Press DOWN to enter water";
			Main.buffName[16] = "Archery";
			Main.buffTip[16] = "20% increased arrow damage and speed";
			Main.buffName[17] = "Hunter";
			Main.buffTip[17] = "Shows the location of enemies";
			Main.buffName[18] = "Gravitation";
			Main.buffTip[18] = "Press UP or DOWN to reverse gravity";
			Main.buffName[19] = "Orb of Light";
			Main.buffTip[19] = "A magical orb that provides light";
			Main.buffName[20] = "Poisoned";
			Main.buffTip[20] = "Slowly losing life";
			Main.buffName[21] = "Potion Sickness";
			Main.buffTip[21] = "Cannot consume anymore healing items";
			Main.buffName[22] = "Darkness";
			Main.buffTip[22] = "Decreased light vision";
			Main.buffName[23] = "Cursed";
			Main.buffTip[23] = "Cannot use any items";
			Main.buffName[24] = "On Fire!";
			Main.buffTip[24] = "Slowly losing life";
			Main.buffName[25] = "Tipsy";
			Main.buffTip[25] = "Increased melee abilities, lowered defense";
			Main.buffName[26] = "Well Fed";
			Main.buffTip[26] = "Minor improvements to all stats";
			for (int i = 0; i < 10; i++)
			{
				Main.recentWorld[i] = "";
				Main.recentIP[i] = "";
				Main.recentPort[i] = 0;
			}
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			int num = Main.rand.Next(15);
			if (num == 0)
			{
				base.Window.Title = "Freeria: Dig Peon, Dig!";
			}
			else
			{
				if (num == 1)
				{
					base.Window.Title = "Freeria: Epic Dirt";
				}
				else
				{
					if (num == 2)
					{
						base.Window.Title = "Freeria: Hey Guys!";
					}
					else
					{
						if (num == 3)
						{
							base.Window.Title = "Freeria: Sand is Overpowered";
						}
						else
						{
							if (num == 4)
							{
								base.Window.Title = "Freeria Part 3: The Return of the Guide";
							}
							else
							{
								if (num == 5)
								{
									base.Window.Title = "Freeria: A Bunnies Tale";
								}
								else
								{
									if (num == 6)
									{
										base.Window.Title = "Freeria: Dr. Bones and The Temple of Blood Moon";
									}
									else
									{
										if (num == 7)
										{
											base.Window.Title = "Freeria: Slimeassic Park";
										}
										else
										{
											if (num == 8)
											{
												base.Window.Title = "Freeria: The Grass is Greener on This Side";
											}
											else
											{
												if (num == 9)
												{
													base.Window.Title = "Freeria: Small Blocks, Not for Children Under the Age of 5";
												}
												else
												{
													if (num == 10)
													{
														base.Window.Title = "Freeria: Digger T' Blocks";
													}
													else
													{
														if (num == 11)
														{
															base.Window.Title = "Freeria: There is No Cow Layer";
														}
														else
														{
															if (num == 12)
															{
																base.Window.Title = "Freeria: Suspicous Looking Eyeballs";
															}
															else
															{
																if (num == 13)
																{
																	base.Window.Title = "Freeria: Purple Grass!";
																}
																else
																{
																	if (num == 14)
																	{
																		base.Window.Title = "Freeria: Noone Dug Behind!";
																	}
																	else
																	{
																		base.Window.Title = "Freeria: Shut Up and Dig Gaiden!";
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			Main.tileShine[22] = 1150;
			Main.tileShine[6] = 1150;
			Main.tileShine[7] = 1100;
			Main.tileShine[8] = 1000;
			Main.tileShine[9] = 1050;
			Main.tileShine[12] = 1000;
			Main.tileShine[21] = 1000;
			Main.tileShine[63] = 900;
			Main.tileShine[64] = 900;
			Main.tileShine[65] = 900;
			Main.tileShine[66] = 900;
			Main.tileShine[67] = 900;
			Main.tileShine[68] = 900;
			Main.tileShine[45] = 1900;
			Main.tileShine[46] = 2000;
			Main.tileShine[47] = 2100;
			Main.tileMergeDirt[1] = true;
			Main.tileMergeDirt[6] = true;
			Main.tileMergeDirt[7] = true;
			Main.tileMergeDirt[8] = true;
			Main.tileMergeDirt[9] = true;
			Main.tileMergeDirt[22] = true;
			Main.tileMergeDirt[25] = true;
			Main.tileMergeDirt[37] = true;
			Main.tileMergeDirt[40] = true;
			Main.tileMergeDirt[53] = true;
			Main.tileMergeDirt[56] = true;
			Main.tileMergeDirt[59] = true;
			Main.tileCut[3] = true;
			Main.tileCut[24] = true;
			Main.tileCut[28] = true;
			Main.tileCut[32] = true;
			Main.tileCut[51] = true;
			Main.tileCut[52] = true;
			Main.tileCut[61] = true;
			Main.tileCut[62] = true;
			Main.tileCut[69] = true;
			Main.tileCut[71] = true;
			Main.tileCut[73] = true;
			Main.tileCut[74] = true;
			Main.tileCut[82] = true;
			Main.tileCut[83] = true;
			Main.tileCut[84] = true;
			Main.tileAlch[82] = true;
			Main.tileAlch[83] = true;
			Main.tileAlch[84] = true;
			Main.tileFrameImportant[82] = true;
			Main.tileFrameImportant[83] = true;
			Main.tileFrameImportant[84] = true;
			Main.tileFrameImportant[85] = true;
			Main.tileFrameImportant[100] = true;
			Main.tileFrameImportant[104] = true;
			Main.tileFrameImportant[105] = true;
			Main.tileLavaDeath[104] = true;
			Main.tileLavaDeath[105] = true;
			Main.tileSolid[0] = true;
			Main.tileBlockLight[0] = true;
			Main.tileSolid[1] = true;
			Main.tileBlockLight[1] = true;
			Main.tileSolid[2] = true;
			Main.tileBlockLight[2] = true;
			Main.tileSolid[3] = false;
			Main.tileNoAttach[3] = true;
			Main.tileNoFail[3] = true;
			Main.tileSolid[4] = false;
			Main.tileNoAttach[4] = true;
			Main.tileNoFail[4] = true;
			Main.tileNoFail[24] = true;
			Main.tileSolid[5] = false;
			Main.tileSolid[6] = true;
			Main.tileBlockLight[6] = true;
			Main.tileSolid[7] = true;
			Main.tileBlockLight[7] = true;
			Main.tileSolid[8] = true;
			Main.tileBlockLight[8] = true;
			Main.tileSolid[9] = true;
			Main.tileBlockLight[9] = true;
			Main.tileBlockLight[10] = true;
			Main.tileSolid[10] = true;
			Main.tileNoAttach[10] = true;
			Main.tileBlockLight[10] = true;
			Main.tileSolid[11] = false;
			Main.tileSolidTop[19] = true;
			Main.tileSolid[19] = true;
			Main.tileSolid[22] = true;
			Main.tileSolid[23] = true;
			Main.tileSolid[25] = true;
			Main.tileSolid[30] = true;
			Main.tileNoFail[32] = true;
			Main.tileBlockLight[32] = true;
			Main.tileSolid[37] = true;
			Main.tileBlockLight[37] = true;
			Main.tileSolid[38] = true;
			Main.tileBlockLight[38] = true;
			Main.tileSolid[39] = true;
			Main.tileBlockLight[39] = true;
			Main.tileSolid[40] = true;
			Main.tileBlockLight[40] = true;
			Main.tileSolid[41] = true;
			Main.tileBlockLight[41] = true;
			Main.tileSolid[43] = true;
			Main.tileBlockLight[43] = true;
			Main.tileSolid[44] = true;
			Main.tileBlockLight[44] = true;
			Main.tileSolid[45] = true;
			Main.tileBlockLight[45] = true;
			Main.tileSolid[46] = true;
			Main.tileBlockLight[46] = true;
			Main.tileSolid[47] = true;
			Main.tileBlockLight[47] = true;
			Main.tileSolid[48] = true;
			Main.tileBlockLight[48] = true;
			Main.tileSolid[53] = true;
			Main.tileBlockLight[53] = true;
			Main.tileSolid[54] = true;
			Main.tileBlockLight[52] = true;
			Main.tileSolid[56] = true;
			Main.tileBlockLight[56] = true;
			Main.tileSolid[57] = true;
			Main.tileBlockLight[57] = true;
			Main.tileSolid[58] = true;
			Main.tileBlockLight[58] = true;
			Main.tileSolid[59] = true;
			Main.tileBlockLight[59] = true;
			Main.tileSolid[60] = true;
			Main.tileBlockLight[60] = true;
			Main.tileSolid[63] = true;
			Main.tileBlockLight[63] = true;
			Main.tileStone[63] = true;
			Main.tileSolid[64] = true;
			Main.tileBlockLight[64] = true;
			Main.tileStone[64] = true;
			Main.tileSolid[65] = true;
			Main.tileBlockLight[65] = true;
			Main.tileStone[65] = true;
			Main.tileSolid[66] = true;
			Main.tileBlockLight[66] = true;
			Main.tileStone[66] = true;
			Main.tileSolid[67] = true;
			Main.tileBlockLight[67] = true;
			Main.tileStone[67] = true;
			Main.tileSolid[68] = true;
			Main.tileBlockLight[68] = true;
			Main.tileStone[68] = true;
			Main.tileSolid[75] = true;
			Main.tileBlockLight[75] = true;
			Main.tileSolid[76] = true;
			Main.tileBlockLight[76] = true;
			Main.tileSolid[70] = true;
			Main.tileBlockLight[70] = true;
			Main.tileBlockLight[51] = true;
			Main.tileNoFail[50] = true;
			Main.tileNoAttach[50] = true;
			Main.tileDungeon[41] = true;
			Main.tileDungeon[43] = true;
			Main.tileDungeon[44] = true;
			Main.tileBlockLight[30] = true;
			Main.tileBlockLight[25] = true;
			Main.tileBlockLight[23] = true;
			Main.tileBlockLight[22] = true;
			Main.tileBlockLight[62] = true;
			Main.tileSolidTop[18] = true;
			Main.tileSolidTop[14] = true;
			Main.tileSolidTop[16] = true;
			Main.tileNoAttach[20] = true;
			Main.tileNoAttach[19] = true;
			Main.tileNoAttach[13] = true;
			Main.tileNoAttach[14] = true;
			Main.tileNoAttach[15] = true;
			Main.tileNoAttach[16] = true;
			Main.tileNoAttach[17] = true;
			Main.tileNoAttach[18] = true;
			Main.tileNoAttach[19] = true;
			Main.tileNoAttach[21] = true;
			Main.tileNoAttach[27] = true;
			Main.tileFrameImportant[3] = true;
			Main.tileFrameImportant[5] = true;
			Main.tileFrameImportant[10] = true;
			Main.tileFrameImportant[11] = true;
			Main.tileFrameImportant[12] = true;
			Main.tileFrameImportant[13] = true;
			Main.tileFrameImportant[14] = true;
			Main.tileFrameImportant[15] = true;
			Main.tileFrameImportant[16] = true;
			Main.tileFrameImportant[17] = true;
			Main.tileFrameImportant[18] = true;
			Main.tileFrameImportant[20] = true;
			Main.tileFrameImportant[21] = true;
			Main.tileFrameImportant[24] = true;
			Main.tileFrameImportant[26] = true;
			Main.tileFrameImportant[27] = true;
			Main.tileFrameImportant[28] = true;
			Main.tileFrameImportant[29] = true;
			Main.tileFrameImportant[31] = true;
			Main.tileFrameImportant[33] = true;
			Main.tileFrameImportant[34] = true;
			Main.tileFrameImportant[35] = true;
			Main.tileFrameImportant[36] = true;
			Main.tileFrameImportant[42] = true;
			Main.tileFrameImportant[50] = true;
			Main.tileFrameImportant[55] = true;
			Main.tileFrameImportant[61] = true;
			Main.tileFrameImportant[71] = true;
			Main.tileFrameImportant[72] = true;
			Main.tileFrameImportant[73] = true;
			Main.tileFrameImportant[74] = true;
			Main.tileFrameImportant[77] = true;
			Main.tileFrameImportant[78] = true;
			Main.tileFrameImportant[79] = true;
			Main.tileFrameImportant[81] = true;
			Main.tileFrameImportant[103] = true;
			Main.tileTable[14] = true;
			Main.tileTable[18] = true;
			Main.tileTable[19] = true;
			Main.tileNoAttach[86] = true;
			Main.tileNoAttach[87] = true;
			Main.tileNoAttach[88] = true;
			Main.tileNoAttach[89] = true;
			Main.tileNoAttach[90] = true;
			Main.tileFrameImportant[86] = true;
			Main.tileFrameImportant[87] = true;
			Main.tileFrameImportant[88] = true;
			Main.tileFrameImportant[89] = true;
			Main.tileFrameImportant[90] = true;
			Main.tileLavaDeath[86] = true;
			Main.tileLavaDeath[87] = true;
			Main.tileLavaDeath[88] = true;
			Main.tileLavaDeath[89] = true;
			Main.tileFrameImportant[101] = true;
			Main.tileLavaDeath[101] = true;
			Main.tileTable[101] = true;
			Main.tileNoAttach[101] = true;
			Main.tileFrameImportant[102] = true;
			Main.tileLavaDeath[102] = true;
			Main.tileNoAttach[102] = true;
			Main.tileNoAttach[94] = true;
			Main.tileNoAttach[95] = true;
			Main.tileNoAttach[96] = true;
			Main.tileNoAttach[97] = true;
			Main.tileNoAttach[98] = true;
			Main.tileNoAttach[99] = true;
			Main.tileFrameImportant[94] = true;
			Main.tileFrameImportant[95] = true;
			Main.tileFrameImportant[96] = true;
			Main.tileFrameImportant[97] = true;
			Main.tileFrameImportant[98] = true;
			Main.tileFrameImportant[99] = true;
			Main.tileFrameImportant[106] = true;
			Main.tileLavaDeath[94] = true;
			Main.tileLavaDeath[95] = true;
			Main.tileLavaDeath[96] = true;
			Main.tileLavaDeath[97] = true;
			Main.tileLavaDeath[98] = true;
			Main.tileLavaDeath[99] = true;
			Main.tileLavaDeath[100] = true;
			Main.tileLavaDeath[103] = true;
			Main.tileTable[87] = true;
			Main.tileTable[88] = true;
			Main.tileSolidTop[87] = true;
			Main.tileSolidTop[88] = true;
			Main.tileSolidTop[101] = true;
			Main.tileNoAttach[91] = true;
			Main.tileFrameImportant[91] = true;
			Main.tileLavaDeath[91] = true;
			Main.tileNoAttach[92] = true;
			Main.tileFrameImportant[92] = true;
			Main.tileLavaDeath[92] = true;
			Main.tileNoAttach[93] = true;
			Main.tileFrameImportant[93] = true;
			Main.tileLavaDeath[93] = true;
			Main.tileWaterDeath[4] = true;
			Main.tileWaterDeath[51] = true;
			Main.tileWaterDeath[93] = true;
			Main.tileWaterDeath[98] = true;
			Main.tileLavaDeath[3] = true;
			Main.tileLavaDeath[5] = true;
			Main.tileLavaDeath[10] = true;
			Main.tileLavaDeath[11] = true;
			Main.tileLavaDeath[12] = true;
			Main.tileLavaDeath[13] = true;
			Main.tileLavaDeath[14] = true;
			Main.tileLavaDeath[15] = true;
			Main.tileLavaDeath[16] = true;
			Main.tileLavaDeath[17] = true;
			Main.tileLavaDeath[18] = true;
			Main.tileLavaDeath[19] = true;
			Main.tileLavaDeath[20] = true;
			Main.tileLavaDeath[27] = true;
			Main.tileLavaDeath[28] = true;
			Main.tileLavaDeath[29] = true;
			Main.tileLavaDeath[32] = true;
			Main.tileLavaDeath[33] = true;
			Main.tileLavaDeath[34] = true;
			Main.tileLavaDeath[35] = true;
			Main.tileLavaDeath[36] = true;
			Main.tileLavaDeath[42] = true;
			Main.tileLavaDeath[49] = true;
			Main.tileLavaDeath[50] = true;
			Main.tileLavaDeath[52] = true;
			Main.tileLavaDeath[55] = true;
			Main.tileLavaDeath[61] = true;
			Main.tileLavaDeath[62] = true;
			Main.tileLavaDeath[69] = true;
			Main.tileLavaDeath[71] = true;
			Main.tileLavaDeath[72] = true;
			Main.tileLavaDeath[73] = true;
			Main.tileLavaDeath[74] = true;
			Main.tileLavaDeath[79] = true;
			Main.tileLavaDeath[80] = true;
			Main.tileLavaDeath[81] = true;
			Main.tileLavaDeath[106] = true;
			Main.wallHouse[1] = true;
			Main.wallHouse[4] = true;
			Main.wallHouse[5] = true;
			Main.wallHouse[6] = true;
			Main.wallHouse[10] = true;
			Main.wallHouse[11] = true;
			Main.wallHouse[12] = true;
			Main.wallHouse[16] = true;
			Main.wallHouse[17] = true;
			Main.wallHouse[18] = true;
			Main.wallHouse[19] = true;
			Main.wallHouse[20] = true;
			Main.tileNoFail[32] = true;
			Main.tileNoFail[61] = true;
			Main.tileNoFail[69] = true;
			Main.tileNoFail[73] = true;
			Main.tileNoFail[74] = true;
			Main.tileNoFail[82] = true;
			Main.tileNoFail[83] = true;
			Main.tileNoFail[84] = true;
			for (int j = 0; j < 107; j++)
			{
				Main.tileName[j] = "";
			}
			Main.tileName[13] = "Bottle";
			Main.tileName[14] = "Table";
			Main.tileName[15] = "Chair";
			Main.tileName[16] = "Anvil";
			Main.tileName[17] = "Furnace";
			Main.tileName[18] = "Workbench";
			Main.tileName[26] = "Demon Altar";
			Main.tileName[77] = "Hellforge";
			Main.tileName[86] = "Loom";
			Main.tileName[94] = "Keg";
			Main.tileName[96] = "Cooking Pot";
			Main.tileName[106] = "Sawmill";
			for (int k = 0; k < Main.maxMenuItems; k++)
			{
				this.menuItemScale[k] = 0.8f;
			}
			for (int l = 0; l < 1001; l++)
			{
				Main.dust[l] = new Dust();
			}
			for (int m = 0; m < 201; m++)
			{
				Main.item[m] = new Item();
			}
			for (int n = 0; n < 1001; n++)
			{
				Main.npc[n] = new NPC();
				Main.npc[n].whoAmI = n;
			}
			for (int num2 = 0; num2 < 256; num2++)
			{
				Main.player[num2] = new Player();
			}
			for (int num3 = 0; num3 < 1001; num3++)
			{
				Main.projectile[num3] = new Projectile();
			}
			for (int num4 = 0; num4 < 201; num4++)
			{
				Main.gore[num4] = new Gore();
			}
			for (int num5 = 0; num5 < 100; num5++)
			{
				Main.cloud[num5] = new Cloud();
			}
			for (int num6 = 0; num6 < 100; num6++)
			{
				Main.combatText[num6] = new CombatText();
			}
			for (int num7 = 0; num7 < 100; num7++)
			{
				Main.itemText[num7] = new ItemText();
			}
			for (int num8 = 0; num8 < 364; num8++)
			{
				Item item = new Item();
				item.SetDefaults(num8, false);
				Main.itemName[num8] = item.name;
			}
			for (int num9 = 0; num9 < Recipe.maxRecipes; num9++)
			{
				Main.recipe[num9] = new Recipe();
				Main.availableRecipeY[num9] = (float)(65 * num9);
			}
			Recipe.SetupRecipes();
			for (int num10 = 0; num10 < Main.numChatLines; num10++)
			{
				Main.chatLine[num10] = new ChatLine();
			}
			for (int num11 = 0; num11 < Liquid.resLiquid; num11++)
			{
				Main.liquid[num11] = new Liquid();
			}
			for (int num12 = 0; num12 < 10000; num12++)
			{
				Main.liquidBuffer[num12] = new LiquidBuffer();
			}
			this.shop[0] = new Chest();
			this.shop[1] = new Chest();
			this.shop[1].SetupShop(1);
			this.shop[2] = new Chest();
			this.shop[2].SetupShop(2);
			this.shop[3] = new Chest();
			this.shop[3].SetupShop(3);
			this.shop[4] = new Chest();
			this.shop[4].SetupShop(4);
			this.shop[5] = new Chest();
			this.shop[5].SetupShop(5);
			Main.teamColor[0] = Color.White;
			Main.teamColor[1] = new Color(230, 40, 20);
			Main.teamColor[2] = new Color(20, 200, 30);
			Main.teamColor[3] = new Color(75, 90, 255);
			Main.teamColor[4] = new Color(200, 180, 0);
			if (Main.menuMode == 1)
			{
				Main.LoadPlayers();
			}
			Netplay.Init();
			if (Main.skipMenu)
			{
				WorldGen.clearWorld();
				Main.gameMenu = false;
				Main.LoadPlayers();
				Main.player[Main.myPlayer] = (Player)Main.loadPlayer[0].Clone();
				Main.PlayerPath = Main.loadPlayerPath[0];
				Main.LoadWorlds();
				WorldGen.generateWorld(-1);
				WorldGen.EveryTileFrame();
				Main.player[Main.myPlayer].Spawn();
			}
			else
			{
				IntPtr systemMenu = Main.GetSystemMenu(base.Window.Handle, false);
				int menuItemCount = Main.GetMenuItemCount(systemMenu);
				Main.RemoveMenu(systemMenu, menuItemCount - 1, 1024);
			}
			if (Main.dedServ)
			{
				return;
			}
			this.graphics.PreferredBackBufferWidth = Main.screenWidth;
			this.graphics.PreferredBackBufferHeight = Main.screenHeight;
			this.graphics.ApplyChanges();
			base.Initialize();
			base.Window.AllowUserResizing = true;
			this.OpenSettings();
			this.OpenRecent();
			Star.SpawnStars();
			foreach (DisplayMode current in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
			{
				if (current.Width >= Main.minScreenW && current.Height >= Main.minScreenH && current.Width <= Main.maxScreenW && current.Height <= Main.maxScreenH)
				{
					bool flag = true;
					for (int num13 = 0; num13 < this.numDisplayModes; num13++)
					{
						if (current.Width == this.displayWidth[num13] && current.Height == this.displayHeight[num13])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.displayHeight[this.numDisplayModes] = current.Height;
						this.displayWidth[this.numDisplayModes] = current.Width;
						this.numDisplayModes++;
					}
				}
			}
			if (Main.autoJoin)
			{
				Main.LoadPlayers();
				Main.menuMode = 1;
				Main.menuMultiplayer = true;
			}
		}
		protected override void LoadContent()
		{
			try
			{
				Main.engine = new AudioEngine("Content\\FreeriaMusic.xgs");
				Main.soundBank = new SoundBank(Main.engine, "Content\\SoundBank.xsb");
				Main.waveBank = new WaveBank(Main.engine, "Content\\WaveBank.xwb");
				for (int i = 1; i < 9; i++)
				{
					Main.music[i] = Main.soundBank.GetCue("Music_" + i);
				}
				Main.soundGrab = base.Content.Load<SoundEffect>("Sounds\\Grab");
				Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
				Main.soundDig[0] = base.Content.Load<SoundEffect>("Sounds\\Dig_0");
				Main.soundInstanceDig[0] = Main.soundDig[0].CreateInstance();
				Main.soundDig[1] = base.Content.Load<SoundEffect>("Sounds\\Dig_1");
				Main.soundInstanceDig[1] = Main.soundDig[1].CreateInstance();
				Main.soundDig[2] = base.Content.Load<SoundEffect>("Sounds\\Dig_2");
				Main.soundInstanceDig[2] = Main.soundDig[2].CreateInstance();
				Main.soundTink[0] = base.Content.Load<SoundEffect>("Sounds\\Tink_0");
				Main.soundInstanceTink[0] = Main.soundTink[0].CreateInstance();
				Main.soundTink[1] = base.Content.Load<SoundEffect>("Sounds\\Tink_1");
				Main.soundInstanceTink[1] = Main.soundTink[1].CreateInstance();
				Main.soundTink[2] = base.Content.Load<SoundEffect>("Sounds\\Tink_2");
				Main.soundInstanceTink[2] = Main.soundTink[2].CreateInstance();
				Main.soundPlayerHit[0] = base.Content.Load<SoundEffect>("Sounds\\Player_Hit_0");
				Main.soundInstancePlayerHit[0] = Main.soundPlayerHit[0].CreateInstance();
				Main.soundPlayerHit[1] = base.Content.Load<SoundEffect>("Sounds\\Player_Hit_1");
				Main.soundInstancePlayerHit[1] = Main.soundPlayerHit[1].CreateInstance();
				Main.soundPlayerHit[2] = base.Content.Load<SoundEffect>("Sounds\\Player_Hit_2");
				Main.soundInstancePlayerHit[2] = Main.soundPlayerHit[2].CreateInstance();
				Main.soundFemaleHit[0] = base.Content.Load<SoundEffect>("Sounds\\Female_Hit_0");
				Main.soundInstanceFemaleHit[0] = Main.soundFemaleHit[0].CreateInstance();
				Main.soundFemaleHit[1] = base.Content.Load<SoundEffect>("Sounds\\Female_Hit_1");
				Main.soundInstanceFemaleHit[1] = Main.soundFemaleHit[1].CreateInstance();
				Main.soundFemaleHit[2] = base.Content.Load<SoundEffect>("Sounds\\Female_Hit_2");
				Main.soundInstanceFemaleHit[2] = Main.soundFemaleHit[2].CreateInstance();
				Main.soundPlayerKilled = base.Content.Load<SoundEffect>("Sounds\\Player_Killed");
				Main.soundInstancePlayerKilled = Main.soundPlayerKilled.CreateInstance();
				Main.soundChat = base.Content.Load<SoundEffect>("Sounds\\Chat");
				Main.soundInstanceChat = Main.soundChat.CreateInstance();
				Main.soundGrass = base.Content.Load<SoundEffect>("Sounds\\Grass");
				Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
				Main.soundDoorOpen = base.Content.Load<SoundEffect>("Sounds\\Door_Opened");
				Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
				Main.soundDoorClosed = base.Content.Load<SoundEffect>("Sounds\\Door_Closed");
				Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
				Main.soundMenuTick = base.Content.Load<SoundEffect>("Sounds\\Menu_Tick");
				Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
				Main.soundMenuOpen = base.Content.Load<SoundEffect>("Sounds\\Menu_Open");
				Main.soundInstanceMenuOpen = Main.soundMenuOpen.CreateInstance();
				Main.soundMenuClose = base.Content.Load<SoundEffect>("Sounds\\Menu_Close");
				Main.soundInstanceMenuClose = Main.soundMenuClose.CreateInstance();
				Main.soundShatter = base.Content.Load<SoundEffect>("Sounds\\Shatter");
				Main.soundInstanceShatter = Main.soundShatter.CreateInstance();
				Main.soundZombie[0] = base.Content.Load<SoundEffect>("Sounds\\Zombie_0");
				Main.soundInstanceZombie[0] = Main.soundZombie[0].CreateInstance();
				Main.soundZombie[1] = base.Content.Load<SoundEffect>("Sounds\\Zombie_1");
				Main.soundInstanceZombie[1] = Main.soundZombie[1].CreateInstance();
				Main.soundZombie[2] = base.Content.Load<SoundEffect>("Sounds\\Zombie_2");
				Main.soundInstanceZombie[2] = Main.soundZombie[2].CreateInstance();
				Main.soundRoar[0] = base.Content.Load<SoundEffect>("Sounds\\Roar_0");
				Main.soundInstanceRoar[0] = Main.soundRoar[0].CreateInstance();
				Main.soundRoar[1] = base.Content.Load<SoundEffect>("Sounds\\Roar_1");
				Main.soundInstanceRoar[1] = Main.soundRoar[1].CreateInstance();
				Main.soundSplash[0] = base.Content.Load<SoundEffect>("Sounds\\Splash_0");
				Main.soundInstanceSplash[0] = Main.soundRoar[0].CreateInstance();
				Main.soundSplash[1] = base.Content.Load<SoundEffect>("Sounds\\Splash_1");
				Main.soundInstanceSplash[1] = Main.soundSplash[1].CreateInstance();
				Main.soundDoubleJump = base.Content.Load<SoundEffect>("Sounds\\Double_Jump");
				Main.soundInstanceDoubleJump = Main.soundRoar[0].CreateInstance();
				Main.soundRun = base.Content.Load<SoundEffect>("Sounds\\Run");
				Main.soundInstanceRun = Main.soundRun.CreateInstance();
				Main.soundCoins = base.Content.Load<SoundEffect>("Sounds\\Coins");
				Main.soundInstanceCoins = Main.soundCoins.CreateInstance();
				Main.soundUnlock = base.Content.Load<SoundEffect>("Sounds\\Unlock");
				Main.soundInstanceUnlock = Main.soundUnlock.CreateInstance();
				Main.soundMaxMana = base.Content.Load<SoundEffect>("Sounds\\MaxMana");
				Main.soundInstanceMaxMana = Main.soundMaxMana.CreateInstance();
				Main.soundDrown = base.Content.Load<SoundEffect>("Sounds\\Drown");
				Main.soundInstanceDrown = Main.soundDrown.CreateInstance();
				for (int j = 1; j < 22; j++)
				{
					Main.soundItem[j] = base.Content.Load<SoundEffect>("Sounds\\Item_" + j);
					Main.soundInstanceItem[j] = Main.soundItem[j].CreateInstance();
				}
				for (int k = 1; k < 4; k++)
				{
					Main.soundNPCHit[k] = base.Content.Load<SoundEffect>("Sounds\\NPC_Hit_" + k);
					Main.soundInstanceNPCHit[k] = Main.soundNPCHit[k].CreateInstance();
				}
				for (int l = 1; l < 6; l++)
				{
					Main.soundNPCKilled[l] = base.Content.Load<SoundEffect>("Sounds\\NPC_Killed_" + l);
					Main.soundInstanceNPCKilled[l] = Main.soundNPCKilled[l].CreateInstance();
				}
			}
			catch
			{
				Main.musicVolume = 0f;
				Main.soundVolume = 0f;
			}
			Main.splashTexture = base.Content.Load<Texture2D>("Images\\splash");
			Main.fadeTexture = base.Content.Load<Texture2D>("Images\\fade-out");
			Main.ghostTexture = base.Content.Load<Texture2D>("Images\\Ghost");
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			for (int m = 0; m < 107; m++)
			{
				Main.tileTexture[m] = base.Content.Load<Texture2D>("Images\\Tiles_" + m);
			}
			for (int n = 1; n < 21; n++)
			{
				Main.wallTexture[n] = base.Content.Load<Texture2D>("Images\\Wall_" + n);
			}
			for (int num = 1; num < 27; num++)
			{
				Main.buffTexture[num] = base.Content.Load<Texture2D>("Images\\Buff_" + num);
			}
			for (int num2 = 0; num2 < 7; num2++)
			{
				Main.backgroundTexture[num2] = base.Content.Load<Texture2D>("Images\\Background_" + num2);
				Main.backgroundWidth[num2] = Main.backgroundTexture[num2].Width;
				Main.backgroundHeight[num2] = Main.backgroundTexture[num2].Height;
			}
			for (int num3 = 0; num3 < 364; num3++)
			{
				Main.itemTexture[num3] = base.Content.Load<Texture2D>("Images\\Item_" + num3);
			}
			for (int num4 = 0; num4 < 74; num4++)
			{
				Main.npcTexture[num4] = base.Content.Load<Texture2D>("Images\\NPC_" + num4);
			}
			for (int num5 = 0; num5 < 56; num5++)
			{
				Main.projectileTexture[num5] = base.Content.Load<Texture2D>("Images\\Projectile_" + num5);
			}
			for (int num6 = 1; num6 < 99; num6++)
			{
				Main.goreTexture[num6] = base.Content.Load<Texture2D>("Images\\Gore_" + num6);
			}
			for (int num7 = 0; num7 < 4; num7++)
			{
				Main.cloudTexture[num7] = base.Content.Load<Texture2D>("Images\\Cloud_" + num7);
			}
			for (int num8 = 0; num8 < 5; num8++)
			{
				Main.starTexture[num8] = base.Content.Load<Texture2D>("Images\\Star_" + num8);
			}
			for (int num9 = 0; num9 < 2; num9++)
			{
				Main.liquidTexture[num9] = base.Content.Load<Texture2D>("Images\\Liquid_" + num9);
			}
			Main.HBLockTexture[0] = base.Content.Load<Texture2D>("Images\\Lock_0");
			Main.HBLockTexture[1] = base.Content.Load<Texture2D>("Images\\Lock_1");
			Main.trashTexture = base.Content.Load<Texture2D>("Images\\Trash");
			Main.cdTexture = base.Content.Load<Texture2D>("Images\\CoolDown");
			Main.logoTexture = base.Content.Load<Texture2D>("Images\\Logo");
			Main.dustTexture = base.Content.Load<Texture2D>("Images\\Dust");
			Main.sunTexture = base.Content.Load<Texture2D>("Images\\Sun");
			Main.sun2Texture = base.Content.Load<Texture2D>("Images\\Sun2");
			Main.moonTexture = base.Content.Load<Texture2D>("Images\\Moon");
			Main.blackTileTexture = base.Content.Load<Texture2D>("Images\\Black_Tile");
			Main.heartTexture = base.Content.Load<Texture2D>("Images\\Heart");
			Main.bubbleTexture = base.Content.Load<Texture2D>("Images\\Bubble");
			Main.manaTexture = base.Content.Load<Texture2D>("Images\\Mana");
			Main.cursorTexture = base.Content.Load<Texture2D>("Images\\Cursor");
			Main.ninjaTexture = base.Content.Load<Texture2D>("Images\\Ninja");
			Main.antLionTexture = base.Content.Load<Texture2D>("Images\\AntlionBody");
			Main.spikeBaseTexture = base.Content.Load<Texture2D>("Images\\Spike_Base");
			Main.treeTopTexture[0] = base.Content.Load<Texture2D>("Images\\Tree_Tops_0");
			Main.treeBranchTexture[0] = base.Content.Load<Texture2D>("Images\\Tree_Branches_0");
			Main.treeTopTexture[1] = base.Content.Load<Texture2D>("Images\\Tree_Tops_1");
			Main.treeBranchTexture[1] = base.Content.Load<Texture2D>("Images\\Tree_Branches_1");
			Main.treeTopTexture[2] = base.Content.Load<Texture2D>("Images\\Tree_Tops_2");
			Main.treeBranchTexture[2] = base.Content.Load<Texture2D>("Images\\Tree_Branches_2");
			Main.shroomCapTexture = base.Content.Load<Texture2D>("Images\\Shroom_Tops");
			Main.inventoryBackTexture = base.Content.Load<Texture2D>("Images\\Inventory_Back");
			Main.inventoryBack2Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back2");
			Main.inventoryBack3Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back3");
			Main.inventoryBack4Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back4");
			Main.inventoryBack5Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back5");
			Main.inventoryBack6Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back6");
			Main.inventoryBack7Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back7");
			Main.inventoryBack8Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back8");
			Main.inventoryBack9Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back9");
			Main.inventoryBack10Texture = base.Content.Load<Texture2D>("Images\\Inventory_Back10");
			Main.textBackTexture = base.Content.Load<Texture2D>("Images\\Text_Back");
			Main.chatTexture = base.Content.Load<Texture2D>("Images\\Chat");
			Main.chat2Texture = base.Content.Load<Texture2D>("Images\\Chat2");
			Main.chatBackTexture = base.Content.Load<Texture2D>("Images\\Chat_Back");
			Main.teamTexture = base.Content.Load<Texture2D>("Images\\Team");
			for (int num10 = 1; num10 < 17; num10++)
			{
				Main.armorBodyTexture[num10] = base.Content.Load<Texture2D>("Images\\Armor_Body_" + num10);
				Main.armorArmTexture[num10] = base.Content.Load<Texture2D>("Images\\Armor_Arm_" + num10);
			}
			for (int num11 = 1; num11 < 29; num11++)
			{
				Main.armorHeadTexture[num11] = base.Content.Load<Texture2D>("Images\\Armor_Head_" + num11);
			}
			for (int num12 = 1; num12 < 16; num12++)
			{
				Main.armorLegTexture[num12] = base.Content.Load<Texture2D>("Images\\Armor_Legs_" + num12);
			}
			for (int num13 = 0; num13 < 36; num13++)
			{
				Main.playerHairTexture[num13] = base.Content.Load<Texture2D>("Images\\Player_Hair_" + (num13 + 1));
			}
			Main.playerEyeWhitesTexture = base.Content.Load<Texture2D>("Images\\Player_Eye_Whites");
			Main.playerEyesTexture = base.Content.Load<Texture2D>("Images\\Player_Eyes");
			Main.playerHandsTexture = base.Content.Load<Texture2D>("Images\\Player_Hands");
			Main.playerHands2Texture = base.Content.Load<Texture2D>("Images\\Player_Hands2");
			Main.playerHeadTexture = base.Content.Load<Texture2D>("Images\\Player_Head");
			Main.playerPantsTexture = base.Content.Load<Texture2D>("Images\\Player_Pants");
			Main.playerShirtTexture = base.Content.Load<Texture2D>("Images\\Player_Shirt");
			Main.playerShoesTexture = base.Content.Load<Texture2D>("Images\\Player_Shoes");
			Main.playerUnderShirtTexture = base.Content.Load<Texture2D>("Images\\Player_Undershirt");
			Main.playerUnderShirt2Texture = base.Content.Load<Texture2D>("Images\\Player_Undershirt2");
			Main.chainTexture = base.Content.Load<Texture2D>("Images\\Chain");
			Main.chain2Texture = base.Content.Load<Texture2D>("Images\\Chain2");
			Main.chain3Texture = base.Content.Load<Texture2D>("Images\\Chain3");
			Main.chain4Texture = base.Content.Load<Texture2D>("Images\\Chain4");
			Main.chain5Texture = base.Content.Load<Texture2D>("Images\\Chain5");
			Main.chain6Texture = base.Content.Load<Texture2D>("Images\\Chain6");
			Main.boneArmTexture = base.Content.Load<Texture2D>("Images\\Arm_Bone");
			Main.fontItemStack = base.Content.Load<SpriteFont>("Fonts\\Item_Stack");
			Main.fontMouseText = base.Content.Load<SpriteFont>("Fonts\\Mouse_Text");
			Main.fontDeathText = base.Content.Load<SpriteFont>("Fonts\\Death_Text");
			Main.fontCombatText[0] = base.Content.Load<SpriteFont>("Fonts\\Combat_Text");
			Main.fontCombatText[1] = base.Content.Load<SpriteFont>("Fonts\\Combat_Crit");
		}
		protected override void UnloadContent()
		{
		}
		protected void UpdateMusic()
		{
			try
			{
				if (!Main.dedServ)
				{
					if (this.curMusic > 0)
					{
						if (!base.IsActive)
						{
							if (!Main.music[this.curMusic].IsPaused && Main.music[this.curMusic].IsPlaying)
							{
								try
								{
									Main.music[this.curMusic].Pause();
								}
								catch
								{
								}
							}
							return;
						}
						if (Main.music[this.curMusic].IsPaused)
						{
							Main.music[this.curMusic].Resume();
						}
					}
					bool flag = false;
					Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
					int num = 5000;
					for (int i = 0; i < 1000; i++)
					{
						if (Main.npc[i].active && (Main.npc[i].boss || Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15 || Main.npc[i].type == 26 || Main.npc[i].type == 27 || Main.npc[i].type == 28 || Main.npc[i].type == 29))
						{
							Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value))
							{
								flag = true;
								break;
							}
						}
					}
					if (Main.musicVolume == 0f)
					{
						this.newMusic = 0;
					}
					else
					{
						if (Main.gameMenu)
						{
							if (Main.netMode != 2)
							{
								this.newMusic = 6;
							}
							else
							{
								this.newMusic = 0;
							}
						}
						else
						{
							if (flag)
							{
								this.newMusic = 5;
							}
							else
							{
								if (Main.player[Main.myPlayer].position.Y > (float)((Main.maxTilesY - 200) * 16))
								{
									this.newMusic = 2;
								}
								else
								{
									if (Main.player[Main.myPlayer].zoneEvil)
									{
										this.newMusic = 8;
									}
									else
									{
										if (Main.player[Main.myPlayer].zoneMeteor || Main.player[Main.myPlayer].zoneDungeon)
										{
											this.newMusic = 2;
										}
										else
										{
											if (Main.player[Main.myPlayer].zoneJungle)
											{
												this.newMusic = 7;
											}
											else
											{
												if ((double)Main.player[Main.myPlayer].position.Y > Main.worldSurface * 16.0 + (double)Main.screenHeight)
												{
													this.newMusic = 4;
												}
												else
												{
													if (Main.dayTime)
													{
														this.newMusic = 1;
													}
													else
													{
														if (!Main.dayTime)
														{
															if (Main.bloodMoon)
															{
																this.newMusic = 2;
															}
															else
															{
																this.newMusic = 3;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					this.curMusic = this.newMusic;
					for (int j = 1; j < 9; j++)
					{
						if (j == this.curMusic)
						{
							if (!Main.music[j].IsPlaying)
							{
								Main.music[j] = Main.soundBank.GetCue("Music_" + j);
								Main.music[j].Play();
								Main.music[j].SetVariable("Volume", Main.musicFade[j] * Main.musicVolume);
							}
							else
							{
								Main.musicFade[j] += 0.005f;
								if (Main.musicFade[j] > 1f)
								{
									Main.musicFade[j] = 1f;
								}
								Main.music[j].SetVariable("Volume", Main.musicFade[j] * Main.musicVolume);
							}
						}
						else
						{
							if (Main.music[j].IsPlaying)
							{
								if (Main.musicFade[this.curMusic] > 0.25f)
								{
									Main.musicFade[j] -= 0.005f;
								}
								else
								{
									if (this.curMusic == 0)
									{
										Main.musicFade[j] = 0f;
									}
								}
								if (Main.musicFade[j] <= 0f)
								{
									Main.musicFade[j] -= 0f;
									Main.music[j].Stop(AudioStopOptions.Immediate);
								}
								else
								{
									Main.music[j].SetVariable("Volume", Main.musicFade[j] * Main.musicVolume);
								}
							}
							else
							{
								Main.musicFade[j] = 0f;
							}
						}
					}
				}
			}
			catch
			{
				Main.musicVolume = 0f;
			}
		}
		protected override void Update(GameTime gameTime)
		{
			if (!Main.dedServ)
			{
				if (Main.fixedTiming)
				{
					if (base.IsActive)
					{
						base.IsFixedTimeStep = false;
						this.graphics.SynchronizeWithVerticalRetrace = true;
					}
					else
					{
						base.IsFixedTimeStep = true;
					}
				}
				else
				{
					base.IsFixedTimeStep = true;
				}
				this.UpdateMusic();
				if (Main.showSplash)
				{
					return;
				}
				if (!Main.gameMenu && Main.netMode == 1)
				{
					if (!Main.saveTime.IsRunning)
					{
						Main.saveTime.Start();
					}
					if (Main.saveTime.ElapsedMilliseconds > 300000L)
					{
						Main.saveTime.Reset();
						WorldGen.saveToonWhilePlaying();
					}
				}
				else
				{
					if (!Main.gameMenu && Main.autoSave)
					{
						if (!Main.saveTime.IsRunning)
						{
							Main.saveTime.Start();
						}
						if (Main.saveTime.ElapsedMilliseconds > 600000L)
						{
							Main.saveTime.Reset();
							WorldGen.saveToonWhilePlaying();
							WorldGen.saveAndPlay();
						}
					}
					else
					{
						if (Main.saveTime.IsRunning)
						{
							Main.saveTime.Stop();
						}
					}
				}
				if (Main.teamCooldown > 0)
				{
					Main.teamCooldown--;
				}
				Main.updateTime++;
				if (Main.updateTime >= 60)
				{
					Main.frameRate = Main.drawTime;
					Main.updateTime = 0;
					Main.drawTime = 0;
					if (Main.frameRate == 60)
					{
						Lighting.lightPasses = 2;
						Lighting.lightSkip = 2;
						Main.cloudLimit = 100;
						Gore.goreTime = 1200;
						Main.numDust = 1000;
					}
					else
					{
						if (Main.frameRate >= 58)
						{
							Lighting.lightPasses = 2;
							Lighting.lightSkip = 2;
							Main.cloudLimit = 100;
							Gore.goreTime = 600;
							Main.numDust = 1000;
						}
						else
						{
							if (Main.frameRate >= 43)
							{
								Lighting.lightPasses = 2;
								Lighting.lightSkip = 3;
								Main.cloudLimit = 75;
								Gore.goreTime = 300;
								Main.numDust = 700;
							}
							else
							{
								if (Main.frameRate >= 28)
								{
									if (!Main.gameMenu)
									{
										Liquid.maxLiquid = 3000;
										Liquid.cycles = 6;
									}
									Lighting.lightPasses = 2;
									Lighting.lightSkip = 4;
									Main.cloudLimit = 50;
									Gore.goreTime = 180;
									Main.numDust = 500;
								}
								else
								{
									Lighting.lightPasses = 2;
									Lighting.lightSkip = 5;
									Main.cloudLimit = 0;
									Gore.goreTime = 0;
									Main.numDust = 200;
								}
							}
						}
					}
					if (Liquid.quickSettle)
					{
						Liquid.maxLiquid = Liquid.resLiquid;
						Liquid.cycles = 1;
					}
					else
					{
						if (Main.frameRate == 60)
						{
							Liquid.maxLiquid = 5000;
							Liquid.cycles = 7;
						}
						else
						{
							if (Main.frameRate >= 58)
							{
								Liquid.maxLiquid = 5000;
								Liquid.cycles = 12;
							}
							else
							{
								if (Main.frameRate >= 43)
								{
									Liquid.maxLiquid = 4000;
									Liquid.cycles = 13;
								}
								else
								{
									if (Main.frameRate >= 28)
									{
										Liquid.maxLiquid = 3500;
										Liquid.cycles = 15;
									}
									else
									{
										Liquid.maxLiquid = 3000;
										Liquid.cycles = 17;
									}
								}
							}
						}
					}
					if (Main.netMode == 2)
					{
						Main.cloudLimit = 0;
					}
				}
				if (!base.IsActive)
				{
					Main.hasFocus = false;
				}
				else
				{
					Main.hasFocus = true;
				}
				if (!base.IsActive && Main.netMode == 0)
				{
					base.IsMouseVisible = true;
					if (Main.netMode != 2 && Main.myPlayer >= 0)
					{
						Main.player[Main.myPlayer].delayUseItem = true;
					}
					Main.mouseLeftRelease = false;
					Main.mouseRightRelease = false;
					if (Main.gameMenu)
					{
						Main.UpdateMenu();
					}
					Main.gamePaused = true;
					return;
				}
				base.IsMouseVisible = false;
				if (Main.keyState.IsKeyDown(Keys.F10) && !Main.chatMode && !Main.editSign)
				{
					if (Main.frameRelease)
					{
						Main.PlaySound(12, -1, -1, 1);
						if (Main.showFrameRate)
						{
							Main.showFrameRate = false;
						}
						else
						{
							Main.showFrameRate = true;
						}
					}
					Main.frameRelease = false;
				}
				else
				{
					Main.frameRelease = true;
				}
				if (Main.keyState.IsKeyDown(Keys.F11))
				{
					if (Main.releaseUI)
					{
						if (Main.hideUI)
						{
							Main.hideUI = false;
						}
						else
						{
							Main.hideUI = true;
						}
					}
					Main.releaseUI = false;
				}
				else
				{
					Main.releaseUI = true;
				}
				if ((Main.keyState.IsKeyDown(Keys.LeftAlt) || Main.keyState.IsKeyDown(Keys.RightAlt)) && Main.keyState.IsKeyDown(Keys.Enter))
				{
					if (this.toggleFullscreen)
					{
						this.graphics.ToggleFullScreen();
						Main.chatRelease = false;
					}
					this.toggleFullscreen = false;
				}
				else
				{
					this.toggleFullscreen = true;
				}
				Main.oldMouseState = Main.mouseState;
				Main.mouseState = Mouse.GetState();
				Main.keyState = Keyboard.GetState();
				if (Main.editSign)
				{
					Main.chatMode = false;
				}
				if (Main.chatMode)
				{
					if (Main.keyState.IsKeyDown(Keys.Escape))
					{
						Main.chatMode = false;
					}
					string a = Main.chatText;
					Main.chatText = Main.GetInputText(Main.chatText);
					while (Main.fontMouseText.MeasureString(Main.chatText).X > 470f)
					{
						Main.chatText = Main.chatText.Substring(0, Main.chatText.Length - 1);
					}
					if (a != Main.chatText)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					if (Main.inputTextEnter && Main.chatRelease)
					{
						if (Main.chatText != "")
						{
							NetMessage.SendData(25, -1, -1, Main.chatText, Main.myPlayer, 0f, 0f, 0f, 0);
						}
						Main.chatText = "";
						Main.chatMode = false;
						Main.chatRelease = false;
						Main.player[Main.myPlayer].releaseHook = false;
						Main.player[Main.myPlayer].releaseThrow = false;
						Main.PlaySound(11, -1, -1, 1);
					}
				}
				if (Main.keyState.IsKeyDown(Keys.Enter) && Main.netMode == 1 && !Main.keyState.IsKeyDown(Keys.LeftAlt) && !Main.keyState.IsKeyDown(Keys.RightAlt))
				{
					if (Main.chatRelease && !Main.chatMode && !Main.editSign && !Main.keyState.IsKeyDown(Keys.Escape))
					{
						Main.PlaySound(10, -1, -1, 1);
						Main.chatMode = true;
						Main.chatText = "";
					}
					Main.chatRelease = false;
				}
				else
				{
					Main.chatRelease = true;
				}
				if (Main.gameMenu)
				{
					Main.UpdateMenu();
					if (Main.netMode != 2)
					{
						return;
					}
					Main.gamePaused = false;
				}
			}
			if (Main.netMode == 1)
			{
				for (int i = 0; i < 44; i++)
				{
					if (Main.player[Main.myPlayer].inventory[i].IsNotTheSameAs(Main.clientPlayer.inventory[i]))
					{
						NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[i].name, Main.myPlayer, (float)i, 0f, 0f, 0);
					}
				}
				if (Main.player[Main.myPlayer].armor[0].IsNotTheSameAs(Main.clientPlayer.armor[0]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 44f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[1].IsNotTheSameAs(Main.clientPlayer.armor[1]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 45f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[2].IsNotTheSameAs(Main.clientPlayer.armor[2]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 46f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[3].IsNotTheSameAs(Main.clientPlayer.armor[3]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 47f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[4].IsNotTheSameAs(Main.clientPlayer.armor[4]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 48f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[5].IsNotTheSameAs(Main.clientPlayer.armor[5]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 49f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[6].IsNotTheSameAs(Main.clientPlayer.armor[6]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 50f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[7].IsNotTheSameAs(Main.clientPlayer.armor[7]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 51f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[8].IsNotTheSameAs(Main.clientPlayer.armor[8]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[8].name, Main.myPlayer, 52f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[9].IsNotTheSameAs(Main.clientPlayer.armor[9]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[9].name, Main.myPlayer, 53f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].armor[10].IsNotTheSameAs(Main.clientPlayer.armor[10]))
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[10].name, Main.myPlayer, 54f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].chest != Main.clientPlayer.chest)
				{
					NetMessage.SendData(33, -1, -1, "", Main.player[Main.myPlayer].chest, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].talkNPC != Main.clientPlayer.talkNPC)
				{
					NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneEvil != Main.clientPlayer.zoneEvil)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneMeteor != Main.clientPlayer.zoneMeteor)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneDungeon != Main.clientPlayer.zoneDungeon)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				if (Main.player[Main.myPlayer].zoneJungle != Main.clientPlayer.zoneJungle)
				{
					NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
				bool flag = false;
				for (int j = 0; j < 10; j++)
				{
					if (Main.player[Main.myPlayer].buffType[j] != Main.clientPlayer.buffType[j])
					{
						flag = true;
					}
				}
				if (flag)
				{
					NetMessage.SendData(50, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
					NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				}
			}
			if (Main.netMode == 1)
			{
				Main.clientPlayer = (Player)Main.player[Main.myPlayer].clientClone();
			}
			if (Main.netMode == 0 && (Main.playerInventory || Main.npcChatText != "" || Main.player[Main.myPlayer].sign >= 0) && Main.autoPause)
			{
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				Main.player[Main.myPlayer].controlInv = false;
				for (int k = 0; k < pressedKeys.Length; k++)
				{
					string a2 = string.Concat(pressedKeys[k]);
					if (a2 == Main.cInv)
					{
						Main.player[Main.myPlayer].controlInv = true;
					}
				}
				if (Main.player[Main.myPlayer].controlInv)
				{
					if (Main.player[Main.myPlayer].releaseInventory)
					{
						Main.player[Main.myPlayer].toggleInv();
					}
					Main.player[Main.myPlayer].releaseInventory = false;
				}
				else
				{
					Main.player[Main.myPlayer].releaseInventory = true;
				}
				if (Main.playerInventory)
				{
					int num = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
					Main.focusRecipe += num;
					if (Main.focusRecipe > Main.numAvailableRecipes - 1)
					{
						Main.focusRecipe = Main.numAvailableRecipes - 1;
					}
					if (Main.focusRecipe < 0)
					{
						Main.focusRecipe = 0;
					}
					Main.player[Main.myPlayer].dropItemCheck();
				}
				Main.player[Main.myPlayer].head = Main.player[Main.myPlayer].armor[0].headSlot;
				Main.player[Main.myPlayer].body = Main.player[Main.myPlayer].armor[1].bodySlot;
				Main.player[Main.myPlayer].legs = Main.player[Main.myPlayer].armor[2].legSlot;
				if (!Main.player[Main.myPlayer].hostile)
				{
					if (Main.player[Main.myPlayer].armor[8].headSlot >= 0)
					{
						Main.player[Main.myPlayer].head = Main.player[Main.myPlayer].armor[8].headSlot;
					}
					if (Main.player[Main.myPlayer].armor[9].bodySlot >= 0)
					{
						Main.player[Main.myPlayer].body = Main.player[Main.myPlayer].armor[9].bodySlot;
					}
					if (Main.player[Main.myPlayer].armor[10].legSlot >= 0)
					{
						Main.player[Main.myPlayer].legs = Main.player[Main.myPlayer].armor[10].legSlot;
					}
				}
				if (Main.editSign)
				{
					if (Main.player[Main.myPlayer].sign == -1)
					{
						Main.editSign = false;
					}
					else
					{
						Main.npcChatText = Main.GetInputText(Main.npcChatText);
						if (Main.inputTextEnter)
						{
							byte[] bytes = new byte[]
							{
								10
							};
							Main.npcChatText += Encoding.ASCII.GetString(bytes);
						}
					}
				}
				Main.gamePaused = true;
				return;
			}
			Main.gamePaused = false;
			if (!Main.dedServ && (double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0 && 255 - Main.tileColor.R - 100 > 0 && Main.netMode != 2)
			{
				Star.UpdateStars();
				Cloud.UpdateClouds();
			}
			int l = 0;
			while (l < 255)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.player[l].UpdatePlayer(l);
						goto IL_101C;
					}
					catch
					{
						goto IL_101C;
					}
					goto IL_100D;
				}
				goto IL_100D;
				IL_101C:
				l++;
				continue;
				IL_100D:
				Main.player[l].UpdatePlayer(l);
				goto IL_101C;
			}
			if (Main.netMode != 1)
			{
				NPC.SpawnNPC();
			}
			for (int m = 0; m < 255; m++)
			{
				Main.player[m].activeNPCs = 0f;
				Main.player[m].townNPCs = 0f;
			}
			int n = 0;
			while (n < 1000)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.npc[n].UpdateNPC(n);
						goto IL_10AC;
					}
					catch (Exception)
					{
						Main.npc[n] = new NPC();
						goto IL_10AC;
					}
					goto IL_109D;
				}
				goto IL_109D;
				IL_10AC:
				n++;
				continue;
				IL_109D:
				Main.npc[n].UpdateNPC(n);
				goto IL_10AC;
			}
			int num2 = 0;
			while (num2 < 200)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.gore[num2].Update();
						goto IL_10F3;
					}
					catch
					{
						Main.gore[num2] = new Gore();
						goto IL_10F3;
					}
					goto IL_10E6;
				}
				goto IL_10E6;
				IL_10F3:
				num2++;
				continue;
				IL_10E6:
				Main.gore[num2].Update();
				goto IL_10F3;
			}
			int num3 = 0;
			while (num3 < 1000)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.projectile[num3].Update(num3);
						goto IL_113E;
					}
					catch
					{
						Main.projectile[num3] = new Projectile();
						goto IL_113E;
					}
					goto IL_112F;
				}
				goto IL_112F;
				IL_113E:
				num3++;
				continue;
				IL_112F:
				Main.projectile[num3].Update(num3);
				goto IL_113E;
			}
			int num4 = 0;
			while (num4 < 200)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						Main.item[num4].UpdateItem(num4);
						goto IL_1189;
					}
					catch
					{
						Main.item[num4] = new Item();
						goto IL_1189;
					}
					goto IL_117A;
				}
				goto IL_117A;
				IL_1189:
				num4++;
				continue;
				IL_117A:
				Main.item[num4].UpdateItem(num4);
				goto IL_1189;
			}
			if (Main.ignoreErrors)
			{
				try
				{
					Dust.UpdateDust();
					goto IL_11CF;
				}
				catch
				{
					for (int num5 = 0; num5 < 1000; num5++)
					{
						Main.dust[num5] = new Dust();
					}
					goto IL_11CF;
				}
			}
			Dust.UpdateDust();
			IL_11CF:
			if (Main.netMode != 2)
			{
				CombatText.UpdateCombatText();
				ItemText.UpdateItemText();
			}
			if (Main.ignoreErrors)
			{
				try
				{
					Main.UpdateTime();
					goto IL_11FD;
				}
				catch
				{
					Main.checkForSpawns = 0;
					goto IL_11FD;
				}
			}
			Main.UpdateTime();
			IL_11FD:
			if (Main.netMode != 1)
			{
				if (Main.ignoreErrors)
				{
					try
					{
						WorldGen.UpdateWorld();
						Main.UpdateInvasion();
						goto IL_1225;
					}
					catch
					{
						goto IL_1225;
					}
				}
				WorldGen.UpdateWorld();
				Main.UpdateInvasion();
			}
			IL_1225:
			if (Main.ignoreErrors)
			{
				try
				{
					if (Main.netMode == 2)
					{
						Main.UpdateServer();
					}
					if (Main.netMode == 1)
					{
						Main.UpdateClient();
					}
					goto IL_126D;
				}
				catch
				{
					int arg_1250_0 = Main.netMode;
					goto IL_126D;
				}
			}
			if (Main.netMode == 2)
			{
				Main.UpdateServer();
				goto IL_1260;
			}
			goto IL_1260;
			IL_126D:
			if (Main.ignoreErrors)
			{
				try
				{
					for (int num6 = 0; num6 < Main.numChatLines; num6++)
					{
						if (Main.chatLine[num6].showTime > 0)
						{
							Main.chatLine[num6].showTime--;
						}
					}
					goto IL_130C;
				}
				catch
				{
					for (int num7 = 0; num7 < Main.numChatLines; num7++)
					{
						Main.chatLine[num7] = new ChatLine();
					}
					goto IL_130C;
				}
				goto IL_12D3;
			}
			goto IL_12D3;
			IL_130C:
			base.Update(gameTime);
			return;
			IL_12D3:
			for (int num8 = 0; num8 < Main.numChatLines; num8++)
			{
				if (Main.chatLine[num8].showTime > 0)
				{
					Main.chatLine[num8].showTime--;
				}
			}
			goto IL_130C;
			IL_1260:
			if (Main.netMode == 1)
			{
				Main.UpdateClient();
				goto IL_126D;
			}
			goto IL_126D;
		}
		private static void UpdateMenu()
		{
			Main.playerInventory = false;
			Main.exitScale = 0.8f;
			if (Main.netMode == 0)
			{
				if (!Main.grabSky)
				{
					Main.time += 86.4;
					if (!Main.dayTime)
					{
						if (Main.time > 32400.0)
						{
							Main.bloodMoon = false;
							Main.time = 0.0;
							Main.dayTime = true;
							Main.moonPhase++;
							if (Main.moonPhase >= 8)
							{
								Main.moonPhase = 0;
								return;
							}
						}
					}
					else
					{
						if (Main.time > 54000.0)
						{
							Main.time = 0.0;
							Main.dayTime = false;
							return;
						}
					}
				}
			}
			else
			{
				if (Main.netMode == 1)
				{
					Main.UpdateTime();
				}
			}
		}
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetKeyState(int keyCode);
		public static string GetInputText(string oldString)
		{
			if (!Main.hasFocus)
			{
				return oldString;
			}
			Main.inputTextEnter = false;
			string text = oldString;
			if (text == null)
			{
				text = "";
			}
			Main.oldInputText = Main.inputText;
			Main.inputText = Keyboard.GetState();
			bool flag = ((ushort)Main.GetKeyState(20) & 65535) != 0;
			bool flag2 = false;
			if (Main.inputText.IsKeyDown(Keys.LeftShift) || Main.inputText.IsKeyDown(Keys.RightShift))
			{
				flag2 = true;
			}
			Keys[] pressedKeys = Main.inputText.GetPressedKeys();
			Keys[] pressedKeys2 = Main.oldInputText.GetPressedKeys();
			bool flag3 = false;
			if (Main.inputText.IsKeyDown(Keys.Back) && Main.oldInputText.IsKeyDown(Keys.Back))
			{
				if (Main.backSpaceCount == 0)
				{
					Main.backSpaceCount = 7;
					flag3 = true;
				}
				Main.backSpaceCount--;
			}
			else
			{
				Main.backSpaceCount = 15;
			}
			for (int i = 0; i < pressedKeys.Length; i++)
			{
				bool flag4 = true;
				for (int j = 0; j < pressedKeys2.Length; j++)
				{
					if (pressedKeys[i] == pressedKeys2[j])
					{
						flag4 = false;
					}
				}
				string text2 = string.Concat(pressedKeys[i]);
				if (text2 == "Back" && (flag4 || flag3))
				{
					if (text.Length > 0)
					{
						text = text.Substring(0, text.Length - 1);
					}
				}
				else
				{
					if (flag4)
					{
						if (text2 == "Space")
						{
							text2 = " ";
						}
						else
						{
							if (text2.Length == 1)
							{
								char c = Convert.ToChar(text2);
								int num = Convert.ToInt32(c);
								if (num >= 65 && num <= 90 && ((!flag2 && !flag) || (flag2 && flag)))
								{
									num += 32;
									c = Convert.ToChar(num);
									text2 = string.Concat(c);
								}
							}
							else
							{
								if (text2.Length == 2 && text2.Substring(0, 1) == "D")
								{
									text2 = text2.Substring(1, 1);
									if (flag2)
									{
										if (text2 == "1")
										{
											text2 = "!";
										}
										if (text2 == "2")
										{
											text2 = "@";
										}
										if (text2 == "3")
										{
											text2 = "#";
										}
										if (text2 == "4")
										{
											text2 = "$";
										}
										if (text2 == "5")
										{
											text2 = "%";
										}
										if (text2 == "6")
										{
											text2 = "^";
										}
										if (text2 == "7")
										{
											text2 = "&";
										}
										if (text2 == "8")
										{
											text2 = "*";
										}
										if (text2 == "9")
										{
											text2 = "(";
										}
										if (text2 == "0")
										{
											text2 = ")";
										}
									}
								}
								else
								{
									if (text2.Length == 7 && text2.Substring(0, 6) == "NumPad")
									{
										text2 = text2.Substring(6, 1);
									}
									else
									{
										if (text2 == "Divide")
										{
											text2 = "/";
										}
										else
										{
											if (text2 == "Multiply")
											{
												text2 = "*";
											}
											else
											{
												if (text2 == "Subtract")
												{
													text2 = "-";
												}
												else
												{
													if (text2 == "Add")
													{
														text2 = "+";
													}
													else
													{
														if (text2 == "Decimal")
														{
															text2 = ".";
														}
														else
														{
															if (text2 == "OemSemicolon")
															{
																text2 = ";";
															}
															else
															{
																if (text2 == "OemPlus")
																{
																	text2 = "=";
																}
																else
																{
																	if (text2 == "OemComma")
																	{
																		text2 = ",";
																	}
																	else
																	{
																		if (text2 == "OemMinus")
																		{
																			text2 = "-";
																		}
																		else
																		{
																			if (text2 == "OemPeriod")
																			{
																				text2 = ".";
																			}
																			else
																			{
																				if (text2 == "OemQuestion")
																				{
																					text2 = "/";
																				}
																				else
																				{
																					if (text2 == "OemTilde")
																					{
																						text2 = "`";
																					}
																					else
																					{
																						if (text2 == "OemOpenBrackets")
																						{
																							text2 = "[";
																						}
																						else
																						{
																							if (text2 == "OemPipe")
																							{
																								text2 = "\\";
																							}
																							else
																							{
																								if (text2 == "OemCloseBrackets")
																								{
																									text2 = "]";
																								}
																								else
																								{
																									if (text2 == "OemQuotes")
																									{
																										text2 = "'";
																									}
																									else
																									{
																										if (text2 == "OemBackslash")
																										{
																											text2 = "\\";
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
															if (flag2)
															{
																if (text2 == ";")
																{
																	text2 = ":";
																}
																else
																{
																	if (text2 == "=")
																	{
																		text2 = "+";
																	}
																	else
																	{
																		if (text2 == ",")
																		{
																			text2 = "<";
																		}
																		else
																		{
																			if (text2 == "-")
																			{
																				text2 = "_";
																			}
																			else
																			{
																				if (text2 == ".")
																				{
																					text2 = ">";
																				}
																				else
																				{
																					if (text2 == "/")
																					{
																						text2 = "?";
																					}
																					else
																					{
																						if (text2 == "`")
																						{
																							text2 = "~";
																						}
																						else
																						{
																							if (text2 == "[")
																							{
																								text2 = "{";
																							}
																							else
																							{
																								if (text2 == "\\")
																								{
																									text2 = "|";
																								}
																								else
																								{
																									if (text2 == "]")
																									{
																										text2 = "}";
																									}
																									else
																									{
																										if (text2 == "'")
																										{
																											text2 = "\"";
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (text2 == "Enter")
						{
							Main.inputTextEnter = true;
						}
						if (text2.Length == 1)
						{
							text += text2;
						}
					}
				}
			}
			return text;
		}
		protected void MouseText(string cursorText, int rare = 0, byte diff = 0)
		{
			if (cursorText == null)
			{
				return;
			}
			int num = Main.mouseState.X + 10;
			int num2 = Main.mouseState.Y + 10;
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			float num10;
			if (Main.toolTip.type > 0)
			{
				rare = Main.toolTip.rare;
				int num3 = 20;
				int num4 = 1;
				string[] array = new string[num3];
				array[0] = Main.toolTip.name;
				if (Main.toolTip.stack > 1)
				{
					string[] array2;
					string[] expr_84 = array2 = array;
					int arg_C8_1 = 0;
					object obj = array2[0];
					expr_84[arg_C8_1] = string.Concat(new object[]
					{
						obj, 
						" (", 
						Main.toolTip.stack, 
						")"
					});
				}
				if (Main.toolTip.social)
				{
					array[num4] = "Equipped in social slot";
					num4++;
					array[num4] = "No stats will be gained";
					num4++;
				}
				else
				{
					if (Main.toolTip.damage > 0)
					{
						int damage = Main.toolTip.damage;
						if (Main.toolTip.melee)
						{
							array[num4] = string.Concat((int)(Main.player[Main.myPlayer].meleeDamage * (float)damage));
							string[] array3;
							IntPtr intPtr;
							(array3 = array)[(int)(intPtr = (IntPtr)num4)] = array3[(int)intPtr] + " melee";
						}
						else
						{
							if (Main.toolTip.ranged)
							{
								array[num4] = string.Concat((int)(Main.player[Main.myPlayer].rangedDamage * (float)damage));
								string[] array4;
								IntPtr intPtr2;
								(array4 = array)[(int)(intPtr2 = (IntPtr)num4)] = array4[(int)intPtr2] + " ranged";
							}
							else
							{
								if (Main.toolTip.magic)
								{
									array[num4] = string.Concat((int)(Main.player[Main.myPlayer].magicDamage * (float)damage));
									string[] array5;
									IntPtr intPtr3;
									(array5 = array)[(int)(intPtr3 = (IntPtr)num4)] = array5[(int)intPtr3] + " magic";
								}
								else
								{
									array[num4] = string.Concat(damage);
								}
							}
						}
						string[] array6;
						IntPtr intPtr4;
						(array6 = array)[(int)(intPtr4 = (IntPtr)num4)] = array6[(int)intPtr4] + " damage";
						num4++;
						if (Main.toolTip.useStyle > 0)
						{
							if (Main.toolTip.useAnimation <= 8)
							{
								array[num4] = "Insanely fast";
							}
							else
							{
								if (Main.toolTip.useAnimation <= 20)
								{
									array[num4] = "Very fast";
								}
								else
								{
									if (Main.toolTip.useAnimation <= 25)
									{
										array[num4] = "Fast";
									}
									else
									{
										if (Main.toolTip.useAnimation <= 30)
										{
											array[num4] = "Average";
										}
										else
										{
											if (Main.toolTip.useAnimation <= 35)
											{
												array[num4] = "Slow";
											}
											else
											{
												if (Main.toolTip.useAnimation <= 45)
												{
													array[num4] = "Very slow";
												}
												else
												{
													if (Main.toolTip.useAnimation <= 55)
													{
														array[num4] = "Extremely slow";
													}
													else
													{
														array[num4] = "Snail";
													}
												}
											}
										}
									}
								}
							}
							string[] array7;
							IntPtr intPtr5;
							(array7 = array)[(int)(intPtr5 = (IntPtr)num4)] = array7[(int)intPtr5] + " speed";
							num4++;
						}
					}
					if (Main.toolTip.headSlot > 0 || Main.toolTip.bodySlot > 0 || Main.toolTip.legSlot > 0 || Main.toolTip.accessory)
					{
						array[num4] = "Equipable";
						num4++;
					}
					if (Main.toolTip.vanity)
					{
						array[num4] = "Vanity Item";
						num4++;
					}
					if (Main.toolTip.defense > 0)
					{
						array[num4] = Main.toolTip.defense + " defense";
						num4++;
					}
					if (Main.toolTip.pick > 0)
					{
						array[num4] = Main.toolTip.pick + "% pickaxe power";
						num4++;
					}
					if (Main.toolTip.axe > 0)
					{
						array[num4] = Main.toolTip.axe * 5 + "% axe power";
						num4++;
					}
					if (Main.toolTip.hammer > 0)
					{
						array[num4] = Main.toolTip.hammer + "% hammer power";
						num4++;
					}
					if (Main.toolTip.healLife > 0)
					{
						array[num4] = "Restores " + Main.toolTip.healLife + " life";
						num4++;
					}
					if (Main.toolTip.healMana > 0)
					{
						array[num4] = "Restores " + Main.toolTip.healMana + " mana";
						num4++;
					}
					if (Main.toolTip.mana > 0 && (Main.toolTip.type != 127 || !Main.player[Main.myPlayer].spaceGun))
					{
						array[num4] = "Uses " + (int)((float)Main.toolTip.mana * Main.player[Main.myPlayer].manaCost) + " mana";
						num4++;
					}
					if (Main.toolTip.createWall > 0 || Main.toolTip.createTile > -1)
					{
						if (Main.toolTip.type != 213)
						{
							array[num4] = "Can be placed";
							num4++;
						}
					}
					else
					{
						if (Main.toolTip.ammo > 0)
						{
							array[num4] = "Ammo";
							num4++;
						}
						else
						{
							if (Main.toolTip.consumable)
							{
								array[num4] = "Consumable";
								num4++;
							}
						}
					}
					if (Main.toolTip.material)
					{
						array[num4] = "Material";
						num4++;
					}
					if (Main.toolTip.toolTip != null)
					{
						array[num4] = Main.toolTip.toolTip;
						num4++;
					}
					if (Main.toolTip.toolTip2 != null)
					{
						array[num4] = Main.toolTip.toolTip2;
						num4++;
					}
					if (Main.toolTip.buffTime > 0)
					{
						string text = "0 s";
						if (Main.toolTip.buffTime / 60 >= 60)
						{
							text = Math.Round((double)(Main.toolTip.buffTime / 60) / 60.0) + " minute duration";
						}
						else
						{
							text = Math.Round((double)Main.toolTip.buffTime / 60.0) + " second duration";
						}
						array[num4] = text;
						num4++;
					}
					if (Main.toolTip.wornArmor && Main.player[Main.myPlayer].setBonus != "")
					{
						array[num4] = "Set bonus: " + Main.player[Main.myPlayer].setBonus;
						num4++;
					}
				}
				if (Main.npcShop > 0)
				{
					if (Main.toolTip.value > 0)
					{
						string text2 = "";
						int num5 = 0;
						int num6 = 0;
						int num7 = 0;
						int num8 = 0;
						int num9 = Main.toolTip.value * Main.toolTip.stack;
						if (!Main.toolTip.buy)
						{
							num9 = Main.toolTip.value / 5 * Main.toolTip.stack;
						}
						if (num9 < 1)
						{
							num9 = 1;
						}
						if (num9 >= 1000000)
						{
							num5 = num9 / 1000000;
							num9 -= num5 * 1000000;
						}
						if (num9 >= 10000)
						{
							num6 = num9 / 10000;
							num9 -= num6 * 10000;
						}
						if (num9 >= 100)
						{
							num7 = num9 / 100;
							num9 -= num7 * 100;
						}
						if (num9 >= 1)
						{
							num8 = num9;
						}
						if (num5 > 0)
						{
							text2 = text2 + num5 + " platinum ";
						}
						if (num6 > 0)
						{
							text2 = text2 + num6 + " gold ";
						}
						if (num7 > 0)
						{
							text2 = text2 + num7 + " silver ";
						}
						if (num8 > 0)
						{
							text2 = text2 + num8 + " copper ";
						}
						if (!Main.toolTip.buy)
						{
							array[num4] = "Sell price: " + text2;
						}
						else
						{
							array[num4] = "Buy price: " + text2;
						}
						num4++;
						num10 = (float)Main.mouseTextColor / 255f;
						if (num5 > 0)
						{
							color = new Color((int)((byte)(220f * num10)), (int)((byte)(220f * num10)), (int)((byte)(198f * num10)), (int)Main.mouseTextColor);
						}
						else
						{
							if (num6 > 0)
							{
								color = new Color((int)((byte)(224f * num10)), (int)((byte)(201f * num10)), (int)((byte)(92f * num10)), (int)Main.mouseTextColor);
							}
							else
							{
								if (num7 > 0)
								{
									color = new Color((int)((byte)(181f * num10)), (int)((byte)(192f * num10)), (int)((byte)(193f * num10)), (int)Main.mouseTextColor);
								}
								else
								{
									if (num8 > 0)
									{
										color = new Color((int)((byte)(246f * num10)), (int)((byte)(138f * num10)), (int)((byte)(96f * num10)), (int)Main.mouseTextColor);
									}
								}
							}
						}
					}
					else
					{
						num10 = (float)Main.mouseTextColor / 255f;
						array[num4] = "No value";
						num4++;
						color = new Color((int)((byte)(120f * num10)), (int)((byte)(120f * num10)), (int)((byte)(120f * num10)), (int)Main.mouseTextColor);
					}
				}
				Vector2 vector = default(Vector2);
				int num11 = 0;
				for (int i = 0; i < num4; i++)
				{
					Vector2 vector2 = Main.fontMouseText.MeasureString(array[i]);
					if (vector2.X > vector.X)
					{
						vector.X = vector2.X;
					}
					vector.Y += vector2.Y + (float)num11;
				}
				if ((float)num + vector.X + 4f > (float)Main.screenWidth)
				{
					num = (int)((float)Main.screenWidth - vector.X - 4f);
				}
				if ((float)num2 + vector.Y + 4f > (float)Main.screenHeight)
				{
					num2 = (int)((float)Main.screenHeight - vector.Y - 4f);
				}
				int num12 = 0;
				num10 = (float)Main.mouseTextColor / 255f;
				for (int j = 0; j < num4; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						int num13 = num;
						int num14 = num2 + num12;
						Color color2 = Color.Black;
						if (k == 0)
						{
							num13 -= 2;
						}
						else
						{
							if (k == 1)
							{
								num13 += 2;
							}
							else
							{
								if (k == 2)
								{
									num14 -= 2;
								}
								else
								{
									if (k == 3)
									{
										num14 += 2;
									}
									else
									{
										color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
										if (j == 0)
										{
											if (rare == 1)
											{
												color2 = new Color((int)((byte)(150f * num10)), (int)((byte)(150f * num10)), (int)((byte)(255f * num10)), (int)Main.mouseTextColor);
											}
											if (rare == 2)
											{
												color2 = new Color((int)((byte)(150f * num10)), (int)((byte)(255f * num10)), (int)((byte)(150f * num10)), (int)Main.mouseTextColor);
											}
											if (rare == 3)
											{
												color2 = new Color((int)((byte)(255f * num10)), (int)((byte)(200f * num10)), (int)((byte)(150f * num10)), (int)Main.mouseTextColor);
											}
											if (rare == 4)
											{
												color2 = new Color((int)((byte)(255f * num10)), (int)((byte)(150f * num10)), (int)((byte)(150f * num10)), (int)Main.mouseTextColor);
											}
											if (diff == 1)
											{
												color2 = new Color((int)((byte)((float)Main.mcColor.R * num10)), (int)((byte)((float)Main.mcColor.G * num10)), (int)((byte)((float)Main.mcColor.B * num10)), (int)Main.mouseTextColor);
											}
											if (diff == 2)
											{
												color2 = new Color((int)((byte)((float)Main.hcColor.R * num10)), (int)((byte)((float)Main.hcColor.G * num10)), (int)((byte)((float)Main.hcColor.B * num10)), (int)Main.mouseTextColor);
											}
										}
										else
										{
											if (j == num4 - 1)
											{
												color2 = color;
											}
										}
									}
								}
							}
						}
						this.spriteBatch.DrawString(Main.fontMouseText, array[j], new Vector2((float)num13, (float)num14), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					num12 += (int)(Main.fontMouseText.MeasureString(array[j]).Y + (float)num11);
				}
				return;
			}
			if (Main.buffString != "" && Main.buffString != null)
			{
				for (int l = 0; l < 5; l++)
				{
					int num15 = num;
					int num16 = num2 + (int)Main.fontMouseText.MeasureString(Main.buffString).Y;
					Color black = Color.Black;
					if (l == 0)
					{
						num15 -= 2;
					}
					else
					{
						if (l == 1)
						{
							num15 += 2;
						}
						else
						{
							if (l == 2)
							{
								num16 -= 2;
							}
							else
							{
								if (l == 3)
								{
									num16 += 2;
								}
								else
								{
									black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
								}
							}
						}
					}
					this.spriteBatch.DrawString(Main.fontMouseText, Main.buffString, new Vector2((float)num15, (float)num16), black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			Vector2 vector3 = Main.fontMouseText.MeasureString(cursorText);
			if ((float)num + vector3.X + 4f > (float)Main.screenWidth)
			{
				num = (int)((float)Main.screenWidth - vector3.X - 4f);
			}
			if ((float)num2 + vector3.Y + 4f > (float)Main.screenHeight)
			{
				num2 = (int)((float)Main.screenHeight - vector3.Y - 4f);
			}
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num, (float)(num2 - 2)), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num, (float)(num2 + 2)), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)(num - 2), (float)num2), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)(num + 2), (float)num2), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			num10 = (float)Main.mouseTextColor / 255f;
			Color color3 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			if (rare == 1)
			{
				color3 = new Color((int)((byte)(150f * num10)), (int)((byte)(150f * num10)), (int)((byte)(255f * num10)), (int)Main.mouseTextColor);
			}
			if (rare == 2)
			{
				color3 = new Color((int)((byte)(150f * num10)), (int)((byte)(255f * num10)), (int)((byte)(150f * num10)), (int)Main.mouseTextColor);
			}
			if (rare == 3)
			{
				color3 = new Color((int)((byte)(255f * num10)), (int)((byte)(200f * num10)), (int)((byte)(150f * num10)), (int)Main.mouseTextColor);
			}
			if (rare == 4)
			{
				color3 = new Color((int)((byte)(255f * num10)), (int)((byte)(150f * num10)), (int)((byte)(150f * num10)), (int)Main.mouseTextColor);
			}
			if (diff == 1)
			{
				color3 = new Color((int)((byte)((float)Main.mcColor.R * num10)), (int)((byte)((float)Main.mcColor.G * num10)), (int)((byte)((float)Main.mcColor.B * num10)), (int)Main.mouseTextColor);
			}
			if (diff == 2)
			{
				color3 = new Color((int)((byte)((float)Main.hcColor.R * num10)), (int)((byte)((float)Main.hcColor.G * num10)), (int)((byte)((float)Main.hcColor.B * num10)), (int)Main.mouseTextColor);
			}
			this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num, (float)num2), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}
		protected void DrawFPS()
		{
			if (Main.showFrameRate)
			{
				string text = string.Concat(Main.frameRate);
				object obj = text;
				text = string.Concat(new object[]
				{
					obj, 
					" (", 
					Liquid.numLiquid + LiquidBuffer.numLiquidBuffer, 
					")"
				});
				this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(4f, (float)(Main.screenHeight - 24)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}
		protected void DrawTiles(bool solidOnly = true)
		{
			int num = (int)(Main.screenPosition.X / 16f - 1f);
			int num2 = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			int num3 = (int)(Main.screenPosition.Y / 16f - 1f);
			int num4 = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			int height = 16;
			int num5 = 16;
			for (int i = num3; i < num4 + 4; i++)
			{
				for (int j = num - 2; j < num2 + 2; j++)
				{
					if (Main.tile[j, i].active && Main.tileSolid[(int)Main.tile[j, i].type] == solidOnly)
					{
						Color color = Lighting.GetColor(j, i);
						int num6 = 0;
						if (Main.tile[j, i].type == 78 || Main.tile[j, i].type == 85)
						{
							num6 = 2;
						}
						if (Main.tile[j, i].type == 33 || Main.tile[j, i].type == 49)
						{
							num6 = -4;
						}
						if (Main.tile[j, i].type == 3 || Main.tile[j, i].type == 4 || Main.tile[j, i].type == 5 || Main.tile[j, i].type == 24 || Main.tile[j, i].type == 33 || Main.tile[j, i].type == 49 || Main.tile[j, i].type == 61 || Main.tile[j, i].type == 71)
						{
							height = 20;
						}
						else
						{
							if (Main.tile[j, i].type == 15 || Main.tile[j, i].type == 14 || Main.tile[j, i].type == 16 || Main.tile[j, i].type == 17 || Main.tile[j, i].type == 18 || Main.tile[j, i].type == 20 || Main.tile[j, i].type == 21 || Main.tile[j, i].type == 26 || Main.tile[j, i].type == 27 || Main.tile[j, i].type == 32 || Main.tile[j, i].type == 69 || Main.tile[j, i].type == 72 || Main.tile[j, i].type == 77 || Main.tile[j, i].type == 80)
							{
								height = 18;
							}
							else
							{
								height = 16;
							}
						}
						if (Main.tile[j, i].type == 4 || Main.tile[j, i].type == 5)
						{
							num5 = 20;
						}
						else
						{
							num5 = 16;
						}
						if (Main.tile[j, i].type == 73 || Main.tile[j, i].type == 74)
						{
							num6 -= 12;
							height = 32;
						}
						if (Main.tile[j, i].type == 81)
						{
							num6 -= 8;
							height = 26;
							num5 = 24;
						}
						if (Main.player[Main.myPlayer].findTreasure && (Main.tile[j, i].type == 6 || Main.tile[j, i].type == 7 || Main.tile[j, i].type == 8 || Main.tile[j, i].type == 9 || Main.tile[j, i].type == 12 || Main.tile[j, i].type == 21 || Main.tile[j, i].type == 22 || Main.tile[j, i].type == 28 || (Main.tile[j, i].type >= 63 && Main.tile[j, i].type <= 68) || Main.tileAlch[(int)Main.tile[j, i].type]))
						{
							if (color.R < Main.mouseTextColor / 2)
							{
								color.R = (byte)(Main.mouseTextColor / 2);
							}
							if (color.G < 70)
							{
								color.G = 70;
							}
							if (color.B < 210)
							{
								color.B = 210;
							}
							color.A = Main.mouseTextColor;
							if (!Main.gamePaused && base.IsActive && Main.rand.Next(150) == 0)
							{
								Vector2 arg_5E2_0 = new Vector2((float)(j * 16), (float)(i * 16));
								int arg_5E2_1 = 16;
								int arg_5E2_2 = 16;
								int arg_5E2_3 = 15;
								float arg_5E2_4 = 0f;
								float arg_5E2_5 = 0f;
								int arg_5E2_6 = 150;
								Color newColor = default(Color);
								int num7 = Dust.NewDust(arg_5E2_0, arg_5E2_1, arg_5E2_2, arg_5E2_3, arg_5E2_4, arg_5E2_5, arg_5E2_6, newColor, 0.8f);
								Dust expr_5F1 = Main.dust[num7];
								expr_5F1.velocity *= 0.1f;
								Main.dust[num7].noLight = true;
							}
						}
						if (!Main.gamePaused && base.IsActive)
						{
							if (Main.tile[j, i].type == 4 && Main.rand.Next(40) == 0)
							{
								if (Main.tile[j, i].frameX == 22)
								{
									Dust.NewDust(new Vector2((float)(j * 16 + 6), (float)(i * 16)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
								}
								if (Main.tile[j, i].frameX == 44)
								{
									Dust.NewDust(new Vector2((float)(j * 16 + 2), (float)(i * 16)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
								}
								else
								{
									Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
								}
							}
							if (Main.tile[j, i].type == 33 && Main.rand.Next(40) == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 - 4)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
							}
							if (Main.tile[j, i].type == 93 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameY == 0)
							{
								Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
							}
							if (Main.tile[j, i].type == 100 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameY == 0)
							{
								if (Main.tile[j, i].frameX == 0)
								{
									if (Main.rand.Next(3) == 0)
									{
										Vector2 arg_894_0 = new Vector2((float)(j * 16 + 4), (float)(i * 16 + 2));
										int arg_894_1 = 4;
										int arg_894_2 = 4;
										int arg_894_3 = 6;
										float arg_894_4 = 0f;
										float arg_894_5 = 0f;
										int arg_894_6 = 100;
										Color newColor = default(Color);
										Dust.NewDust(arg_894_0, arg_894_1, arg_894_2, arg_894_3, arg_894_4, arg_894_5, arg_894_6, newColor, 1f);
									}
									else
									{
										Vector2 arg_8D3_0 = new Vector2((float)(j * 16 + 14), (float)(i * 16 + 2));
										int arg_8D3_1 = 4;
										int arg_8D3_2 = 4;
										int arg_8D3_3 = 6;
										float arg_8D3_4 = 0f;
										float arg_8D3_5 = 0f;
										int arg_8D3_6 = 100;
										Color newColor = default(Color);
										Dust.NewDust(arg_8D3_0, arg_8D3_1, arg_8D3_2, arg_8D3_3, arg_8D3_4, arg_8D3_5, arg_8D3_6, newColor, 1f);
									}
								}
								else
								{
									if (Main.rand.Next(3) == 0)
									{
										Vector2 arg_91B_0 = new Vector2((float)(j * 16 + 6), (float)(i * 16 + 2));
										int arg_91B_1 = 4;
										int arg_91B_2 = 4;
										int arg_91B_3 = 6;
										float arg_91B_4 = 0f;
										float arg_91B_5 = 0f;
										int arg_91B_6 = 100;
										Color newColor = default(Color);
										Dust.NewDust(arg_91B_0, arg_91B_1, arg_91B_2, arg_91B_3, arg_91B_4, arg_91B_5, arg_91B_6, newColor, 1f);
									}
									else
									{
										Vector2 arg_954_0 = new Vector2((float)(j * 16), (float)(i * 16 + 2));
										int arg_954_1 = 4;
										int arg_954_2 = 4;
										int arg_954_3 = 6;
										float arg_954_4 = 0f;
										float arg_954_5 = 0f;
										int arg_954_6 = 100;
										Color newColor = default(Color);
										Dust.NewDust(arg_954_0, arg_954_1, arg_954_2, arg_954_3, arg_954_4, arg_954_5, arg_954_6, newColor, 1f);
									}
								}
							}
							if (Main.tile[j, i].type == 98 && Main.rand.Next(40) == 0 && Main.tile[j, i].frameY == 0 && Main.tile[j, i].frameX == 0)
							{
								Vector2 arg_9DD_0 = new Vector2((float)(j * 16 + 12), (float)(i * 16 + 2));
								int arg_9DD_1 = 4;
								int arg_9DD_2 = 4;
								int arg_9DD_3 = 6;
								float arg_9DD_4 = 0f;
								float arg_9DD_5 = 0f;
								int arg_9DD_6 = 100;
								Color newColor = default(Color);
								Dust.NewDust(arg_9DD_0, arg_9DD_1, arg_9DD_2, arg_9DD_3, arg_9DD_4, arg_9DD_5, arg_9DD_6, newColor, 1f);
							}
							if (Main.tile[j, i].type == 49 && Main.rand.Next(20) == 0)
							{
								Vector2 arg_A3C_0 = new Vector2((float)(j * 16 + 4), (float)(i * 16 - 4));
								int arg_A3C_1 = 4;
								int arg_A3C_2 = 4;
								int arg_A3C_3 = 29;
								float arg_A3C_4 = 0f;
								float arg_A3C_5 = 0f;
								int arg_A3C_6 = 100;
								Color newColor = default(Color);
								Dust.NewDust(arg_A3C_0, arg_A3C_1, arg_A3C_2, arg_A3C_3, arg_A3C_4, arg_A3C_5, arg_A3C_6, newColor, 1f);
							}
							if ((Main.tile[j, i].type == 34 || Main.tile[j, i].type == 35 || Main.tile[j, i].type == 36) && Main.rand.Next(40) == 0 && Main.tile[j, i].frameY == 18 && (Main.tile[j, i].frameX == 0 || Main.tile[j, i].frameX == 36))
							{
								Vector2 arg_B0D_0 = new Vector2((float)(j * 16), (float)(i * 16 + 2));
								int arg_B0D_1 = 14;
								int arg_B0D_2 = 6;
								int arg_B0D_3 = 6;
								float arg_B0D_4 = 0f;
								float arg_B0D_5 = 0f;
								int arg_B0D_6 = 100;
								Color newColor = default(Color);
								Dust.NewDust(arg_B0D_0, arg_B0D_1, arg_B0D_2, arg_B0D_3, arg_B0D_4, arg_B0D_5, arg_B0D_6, newColor, 1f);
							}
							if (Main.tile[j, i].type == 22 && Main.rand.Next(400) == 0)
							{
								Vector2 arg_B6C_0 = new Vector2((float)(j * 16), (float)(i * 16));
								int arg_B6C_1 = 16;
								int arg_B6C_2 = 16;
								int arg_B6C_3 = 14;
								float arg_B6C_4 = 0f;
								float arg_B6C_5 = 0f;
								int arg_B6C_6 = 0;
								Color newColor = default(Color);
								Dust.NewDust(arg_B6C_0, arg_B6C_1, arg_B6C_2, arg_B6C_3, arg_B6C_4, arg_B6C_5, arg_B6C_6, newColor, 1f);
							}
							else
							{
								if ((Main.tile[j, i].type == 23 || Main.tile[j, i].type == 24 || Main.tile[j, i].type == 32) && Main.rand.Next(500) == 0)
								{
									Vector2 arg_BFE_0 = new Vector2((float)(j * 16), (float)(i * 16));
									int arg_BFE_1 = 16;
									int arg_BFE_2 = 16;
									int arg_BFE_3 = 14;
									float arg_BFE_4 = 0f;
									float arg_BFE_5 = 0f;
									int arg_BFE_6 = 0;
									Color newColor = default(Color);
									Dust.NewDust(arg_BFE_0, arg_BFE_1, arg_BFE_2, arg_BFE_3, arg_BFE_4, arg_BFE_5, arg_BFE_6, newColor, 1f);
								}
								else
								{
									if (Main.tile[j, i].type == 25 && Main.rand.Next(700) == 0)
									{
										Vector2 arg_C62_0 = new Vector2((float)(j * 16), (float)(i * 16));
										int arg_C62_1 = 16;
										int arg_C62_2 = 16;
										int arg_C62_3 = 14;
										float arg_C62_4 = 0f;
										float arg_C62_5 = 0f;
										int arg_C62_6 = 0;
										Color newColor = default(Color);
										Dust.NewDust(arg_C62_0, arg_C62_1, arg_C62_2, arg_C62_3, arg_C62_4, arg_C62_5, arg_C62_6, newColor, 1f);
									}
									else
									{
										if (Main.tile[j, i].type == 31 && Main.rand.Next(20) == 0)
										{
											Vector2 arg_CC4_0 = new Vector2((float)(j * 16), (float)(i * 16));
											int arg_CC4_1 = 16;
											int arg_CC4_2 = 16;
											int arg_CC4_3 = 14;
											float arg_CC4_4 = 0f;
											float arg_CC4_5 = 0f;
											int arg_CC4_6 = 100;
											Color newColor = default(Color);
											Dust.NewDust(arg_CC4_0, arg_CC4_1, arg_CC4_2, arg_CC4_3, arg_CC4_4, arg_CC4_5, arg_CC4_6, newColor, 1f);
										}
										else
										{
											if (Main.tile[j, i].type == 26 && Main.rand.Next(20) == 0)
											{
												Vector2 arg_D26_0 = new Vector2((float)(j * 16), (float)(i * 16));
												int arg_D26_1 = 16;
												int arg_D26_2 = 16;
												int arg_D26_3 = 14;
												float arg_D26_4 = 0f;
												float arg_D26_5 = 0f;
												int arg_D26_6 = 100;
												Color newColor = default(Color);
												Dust.NewDust(arg_D26_0, arg_D26_1, arg_D26_2, arg_D26_3, arg_D26_4, arg_D26_5, arg_D26_6, newColor, 1f);
											}
											else
											{
												if ((Main.tile[j, i].type == 71 || Main.tile[j, i].type == 72) && Main.rand.Next(500) == 0)
												{
													Vector2 arg_DA5_0 = new Vector2((float)(j * 16), (float)(i * 16));
													int arg_DA5_1 = 16;
													int arg_DA5_2 = 16;
													int arg_DA5_3 = 41;
													float arg_DA5_4 = 0f;
													float arg_DA5_5 = 0f;
													int arg_DA5_6 = 250;
													Color newColor = default(Color);
													Dust.NewDust(arg_DA5_0, arg_DA5_1, arg_DA5_2, arg_DA5_3, arg_DA5_4, arg_DA5_5, arg_DA5_6, newColor, 0.8f);
												}
												else
												{
													if ((Main.tile[j, i].type == 17 || Main.tile[j, i].type == 77) && Main.rand.Next(40) == 0)
													{
														if (Main.tile[j, i].frameX == 18 & Main.tile[j, i].frameY == 18)
														{
															Vector2 arg_E51_0 = new Vector2((float)(j * 16 + 2), (float)(i * 16));
															int arg_E51_1 = 8;
															int arg_E51_2 = 6;
															int arg_E51_3 = 6;
															float arg_E51_4 = 0f;
															float arg_E51_5 = 0f;
															int arg_E51_6 = 100;
															Color newColor = default(Color);
															Dust.NewDust(arg_E51_0, arg_E51_1, arg_E51_2, arg_E51_3, arg_E51_4, arg_E51_5, arg_E51_6, newColor, 1f);
														}
													}
													else
													{
														if (Main.tile[j, i].type == 37 && Main.rand.Next(250) == 0)
														{
															Vector2 arg_EBB_0 = new Vector2((float)(j * 16), (float)(i * 16));
															int arg_EBB_1 = 16;
															int arg_EBB_2 = 16;
															int arg_EBB_3 = 6;
															float arg_EBB_4 = 0f;
															float arg_EBB_5 = 0f;
															int arg_EBB_6 = 0;
															Color newColor = default(Color);
															int num8 = Dust.NewDust(arg_EBB_0, arg_EBB_1, arg_EBB_2, arg_EBB_3, arg_EBB_4, arg_EBB_5, arg_EBB_6, newColor, (float)Main.rand.Next(3));
															if (Main.dust[num8].scale > 1f)
															{
																Main.dust[num8].noGravity = true;
															}
														}
														else
														{
															if ((Main.tile[j, i].type == 58 || Main.tile[j, i].type == 76) && Main.rand.Next(250) == 0)
															{
																Vector2 arg_F65_0 = new Vector2((float)(j * 16), (float)(i * 16));
																int arg_F65_1 = 16;
																int arg_F65_2 = 16;
																int arg_F65_3 = 6;
																float arg_F65_4 = 0f;
																float arg_F65_5 = 0f;
																int arg_F65_6 = 0;
																Color newColor = default(Color);
																int num9 = Dust.NewDust(arg_F65_0, arg_F65_1, arg_F65_2, arg_F65_3, arg_F65_4, arg_F65_5, arg_F65_6, newColor, (float)Main.rand.Next(3));
																if (Main.dust[num9].scale > 1f)
																{
																	Main.dust[num9].noGravity = true;
																}
																Main.dust[num9].noLight = true;
															}
															else
															{
																if (Main.tile[j, i].type == 61)
																{
																	if (Main.tile[j, i].frameX == 144)
																	{
																		if (Main.rand.Next(60) == 0)
																		{
																			Vector2 arg_101B_0 = new Vector2((float)(j * 16), (float)(i * 16));
																			int arg_101B_1 = 16;
																			int arg_101B_2 = 16;
																			int arg_101B_3 = 44;
																			float arg_101B_4 = 0f;
																			float arg_101B_5 = 0f;
																			int arg_101B_6 = 250;
																			Color newColor = default(Color);
																			int num10 = Dust.NewDust(arg_101B_0, arg_101B_1, arg_101B_2, arg_101B_3, arg_101B_4, arg_101B_5, arg_101B_6, newColor, 0.4f);
																			Main.dust[num10].fadeIn = 0.7f;
																		}
																		color.A = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																		color.R = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																		color.B = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																		color.G = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
																	}
																}
																else
																{
																	if (Main.tileShine[(int)Main.tile[j, i].type] > 0 && color.R > 60 && Main.rand.Next(Main.tileShine[(int)Main.tile[j, i].type]) < (int)(color.R / 50) && (Main.tile[j, i].type != 21 || (Main.tile[j, i].frameX >= 36 && Main.tile[j, i].frameX < 180)))
																	{
																		Vector2 arg_117D_0 = new Vector2((float)(j * 16), (float)(i * 16));
																		int arg_117D_1 = 16;
																		int arg_117D_2 = 16;
																		int arg_117D_3 = 43;
																		float arg_117D_4 = 0f;
																		float arg_117D_5 = 0f;
																		int arg_117D_6 = 254;
																		Color newColor = default(Color);
																		int num11 = Dust.NewDust(arg_117D_0, arg_117D_1, arg_117D_2, arg_117D_3, arg_117D_4, arg_117D_5, arg_117D_6, newColor, 0.5f);
																		Dust expr_118C = Main.dust[num11];
																		expr_118C.velocity *= 0f;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[j, i].type == 5 && Main.tile[j, i].frameY >= 198 && Main.tile[j, i].frameX >= 22)
						{
							int num12 = 0;
							if (Main.tile[j, i].frameX == 22)
							{
								if (Main.tile[j, i].frameY == 220)
								{
									num12 = 1;
								}
								else
								{
									if (Main.tile[j, i].frameY == 242)
									{
										num12 = 2;
									}
								}
								int num13 = 0;
								int num14 = 80;
								int num15 = 80;
								int num16 = 32;
								for (int k = i; k < i + 100; k++)
								{
									if (Main.tile[j, k].type == 2)
									{
										num13 = 0;
										break;
									}
									if (Main.tile[j, k].type == 23)
									{
										num13 = 1;
										break;
									}
									if (Main.tile[j, k].type == 60)
									{
										num13 = 2;
										num14 = 114;
										num15 = 96;
										num16 = 48;
										break;
									}
								}
								SpriteBatch arg_1346_0 = this.spriteBatch;
								Texture2D arg_1346_1 = Main.treeTopTexture[num13];
								Vector2 arg_1346_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X - num16), (float)(i * 16 - (int)Main.screenPosition.Y - num15 + 16));
								Rectangle? arg_1346_3 = new Rectangle?(new Rectangle(num12 * (num14 + 2), 0, num14, num15));
								Color arg_1346_4 = Lighting.GetColor(j, i);
								float arg_1346_5 = 0f;
								Vector2 origin = default(Vector2);
								arg_1346_0.Draw(arg_1346_1, arg_1346_2, arg_1346_3, arg_1346_4, arg_1346_5, origin, 1f, SpriteEffects.None, 0f);
							}
							else
							{
								if (Main.tile[j, i].frameX == 44)
								{
									if (Main.tile[j, i].frameY == 220)
									{
										num12 = 1;
									}
									else
									{
										if (Main.tile[j, i].frameY == 242)
										{
											num12 = 2;
										}
									}
									int num17 = 0;
									for (int l = i; l < i + 100; l++)
									{
										if (Main.tile[j + 1, l].type == 2)
										{
											num17 = 0;
											break;
										}
										if (Main.tile[j + 1, l].type == 23)
										{
											num17 = 1;
											break;
										}
										if (Main.tile[j + 1, l].type == 60)
										{
											num17 = 2;
											break;
										}
									}
									SpriteBatch arg_148B_0 = this.spriteBatch;
									Texture2D arg_148B_1 = Main.treeBranchTexture[num17];
									Vector2 arg_148B_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X - 24), (float)(i * 16 - (int)Main.screenPosition.Y - 12));
									Rectangle? arg_148B_3 = new Rectangle?(new Rectangle(0, num12 * 42, 40, 40));
									Color arg_148B_4 = Lighting.GetColor(j, i);
									float arg_148B_5 = 0f;
									Vector2 origin = default(Vector2);
									arg_148B_0.Draw(arg_148B_1, arg_148B_2, arg_148B_3, arg_148B_4, arg_148B_5, origin, 1f, SpriteEffects.None, 0f);
								}
								else
								{
									if (Main.tile[j, i].frameX == 66)
									{
										if (Main.tile[j, i].frameY == 220)
										{
											num12 = 1;
										}
										else
										{
											if (Main.tile[j, i].frameY == 242)
											{
												num12 = 2;
											}
										}
										int num18 = 0;
										for (int m = i; m < i + 100; m++)
										{
											if (Main.tile[j - 1, m].type == 2)
											{
												num18 = 0;
												break;
											}
											if (Main.tile[j - 1, m].type == 23)
											{
												num18 = 1;
												break;
											}
											if (Main.tile[j - 1, m].type == 60)
											{
												num18 = 2;
												break;
											}
										}
										SpriteBatch arg_15CE_0 = this.spriteBatch;
										Texture2D arg_15CE_1 = Main.treeBranchTexture[num18];
										Vector2 arg_15CE_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)(i * 16 - (int)Main.screenPosition.Y - 12));
										Rectangle? arg_15CE_3 = new Rectangle?(new Rectangle(42, num12 * 42, 40, 40));
										Color arg_15CE_4 = Lighting.GetColor(j, i);
										float arg_15CE_5 = 0f;
										Vector2 origin = default(Vector2);
										arg_15CE_0.Draw(arg_15CE_1, arg_15CE_2, arg_15CE_3, arg_15CE_4, arg_15CE_5, origin, 1f, SpriteEffects.None, 0f);
									}
								}
							}
						}
						if (Main.tile[j, i].type == 72 && Main.tile[j, i].frameX >= 36)
						{
							int num19 = 0;
							if (Main.tile[j, i].frameY == 18)
							{
								num19 = 1;
							}
							else
							{
								if (Main.tile[j, i].frameY == 36)
								{
									num19 = 2;
								}
							}
							SpriteBatch arg_16B1_0 = this.spriteBatch;
							Texture2D arg_16B1_1 = Main.shroomCapTexture;
							Vector2 arg_16B1_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X - 22), (float)(i * 16 - (int)Main.screenPosition.Y - 26));
							Rectangle? arg_16B1_3 = new Rectangle?(new Rectangle(num19 * 62, 0, 60, 42));
							Color arg_16B1_4 = Lighting.GetColor(j, i);
							float arg_16B1_5 = 0f;
							Vector2 origin = default(Vector2);
							arg_16B1_0.Draw(arg_16B1_1, arg_16B1_2, arg_16B1_3, arg_16B1_4, arg_16B1_5, origin, 1f, SpriteEffects.None, 0f);
						}
						if (color.R > 0)
						{
							if (solidOnly && Main.tileSolid[(int)Main.tile[j, i].type] && !Main.tileSolidTop[(int)Main.tile[j, i].type] && (Main.tile[j - 1, i].liquid > 0 || Main.tile[j + 1, i].liquid > 0 || Main.tile[j, i - 1].liquid > 0 || Main.tile[j, i + 1].liquid > 0))
							{
								Color color2 = Lighting.GetColor(j, i);
								int num20 = 0;
								bool flag = false;
								bool flag2 = false;
								bool flag3 = false;
								bool flag4 = false;
								int num21 = 0;
								bool flag5 = false;
								if ((int)Main.tile[j - 1, i].liquid > num20)
								{
									num20 = (int)Main.tile[j - 1, i].liquid;
									flag = true;
								}
								else
								{
									if (Main.tile[j - 1, i].liquid > 0)
									{
										flag = true;
									}
								}
								if ((int)Main.tile[j + 1, i].liquid > num20)
								{
									num20 = (int)Main.tile[j + 1, i].liquid;
									flag2 = true;
								}
								else
								{
									if (Main.tile[j + 1, i].liquid > 0)
									{
										num20 = (int)Main.tile[j + 1, i].liquid;
										flag2 = true;
									}
								}
								if (Main.tile[j, i - 1].liquid > 0)
								{
									flag3 = true;
								}
								if (Main.tile[j, i + 1].liquid > 240)
								{
									flag4 = true;
								}
								if (Main.tile[j - 1, i].liquid > 0)
								{
									if (Main.tile[j - 1, i].lava)
									{
										num21 = 1;
									}
									else
									{
										flag5 = true;
									}
								}
								if (Main.tile[j + 1, i].liquid > 0)
								{
									if (Main.tile[j + 1, i].lava)
									{
										num21 = 1;
									}
									else
									{
										flag5 = true;
									}
								}
								if (Main.tile[j, i - 1].liquid > 0)
								{
									if (Main.tile[j, i - 1].lava)
									{
										num21 = 1;
									}
									else
									{
										flag5 = true;
									}
								}
								if (Main.tile[j, i + 1].liquid > 0)
								{
									if (Main.tile[j, i + 1].lava)
									{
										num21 = 1;
									}
									else
									{
										flag5 = true;
									}
								}
								if (!flag5 || num21 != 1)
								{
									Vector2 value = new Vector2((float)(j * 16), (float)(i * 16));
									Rectangle value2 = new Rectangle(0, 4, 16, 16);
									if (flag4 && (flag || flag2))
									{
										flag = true;
										flag2 = true;
									}
									if ((!flag3 || (!flag && !flag2)) && (!flag4 || !flag3))
									{
										if (flag3)
										{
											value2 = new Rectangle(0, 4, 16, 4);
										}
										else
										{
											if (flag4 && !flag && !flag2)
											{
												value = new Vector2((float)(j * 16), (float)(i * 16 + 12));
												value2 = new Rectangle(0, 4, 16, 4);
											}
											else
											{
												float num22 = (float)(256 - num20);
												num22 /= 32f;
												if (flag && flag2)
												{
													value = new Vector2((float)(j * 16), (float)(i * 16 + (int)num22 * 2));
													value2 = new Rectangle(0, 4, 16, 16 - (int)num22 * 2);
												}
												else
												{
													if (flag)
													{
														value = new Vector2((float)(j * 16), (float)(i * 16 + (int)num22 * 2));
														value2 = new Rectangle(0, 4, 4, 16 - (int)num22 * 2);
													}
													else
													{
														value = new Vector2((float)(j * 16 + 12), (float)(i * 16 + (int)num22 * 2));
														value2 = new Rectangle(0, 4, 4, 16 - (int)num22 * 2);
													}
												}
											}
										}
									}
									float num23 = 0.5f;
									if (num21 == 1)
									{
										num23 *= 1.6f;
									}
									if ((double)i < Main.worldSurface || num23 > 1f)
									{
										num23 = 1f;
									}
									float num24 = (float)color2.R * num23;
									float num25 = (float)color2.G * num23;
									float num26 = (float)color2.B * num23;
									float num27 = (float)color2.A * num23;
									color2 = new Color((int)((byte)num24), (int)((byte)num25), (int)((byte)num26), (int)((byte)num27));
									SpriteBatch arg_1B5D_0 = this.spriteBatch;
									Texture2D arg_1B5D_1 = Main.liquidTexture[num21];
									Vector2 arg_1B5D_2 = value - Main.screenPosition;
									Rectangle? arg_1B5D_3 = new Rectangle?(value2);
									Color arg_1B5D_4 = color2;
									float arg_1B5D_5 = 0f;
									Vector2 origin = default(Vector2);
									arg_1B5D_0.Draw(arg_1B5D_1, arg_1B5D_2, arg_1B5D_3, arg_1B5D_4, arg_1B5D_5, origin, 1f, SpriteEffects.None, 0f);
								}
							}
							if (Main.tile[j, i].type == 51)
							{
								Color color3 = Lighting.GetColor(j, i);
								float num28 = 0.5f;
								float num29 = (float)color3.R * num28;
								float num30 = (float)color3.G * num28;
								float num31 = (float)color3.B * num28;
								float num32 = (float)color3.A * num28;
								color3 = new Color((int)((byte)num29), (int)((byte)num30), (int)((byte)num31), (int)((byte)num32));
								SpriteBatch arg_1C80_0 = this.spriteBatch;
								Texture2D arg_1C80_1 = Main.tileTexture[(int)Main.tile[j, i].type];
								Vector2 arg_1C80_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num5 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num6));
								Rectangle? arg_1C80_3 = new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num5, height));
								Color arg_1C80_4 = color3;
								float arg_1C80_5 = 0f;
								Vector2 origin = default(Vector2);
								arg_1C80_0.Draw(arg_1C80_1, arg_1C80_2, arg_1C80_3, arg_1C80_4, arg_1C80_5, origin, 1f, SpriteEffects.None, 0f);
							}
							else
							{
								if (Main.tileAlch[(int)Main.tile[j, i].type])
								{
									height = 20;
									num6 = -1;
									int num33 = (int)Main.tile[j, i].type;
									int num34 = (int)(Main.tile[j, i].frameX / 18);
									if (num33 > 82)
									{
										if (num34 == 0 && Main.dayTime)
										{
											num33 = 84;
										}
										if (num34 == 1 && !Main.dayTime)
										{
											num33 = 84;
										}
										if (num34 == 3 && Main.bloodMoon)
										{
											num33 = 84;
										}
									}
									if (num33 == 84)
									{
										if (num34 == 0 && Main.rand.Next(100) == 0)
										{
											Vector2 arg_1D69_0 = new Vector2((float)(j * 16), (float)(i * 16 - 4));
											int arg_1D69_1 = 16;
											int arg_1D69_2 = 16;
											int arg_1D69_3 = 19;
											float arg_1D69_4 = 0f;
											float arg_1D69_5 = 0f;
											int arg_1D69_6 = 160;
											Color newColor = default(Color);
											int num35 = Dust.NewDust(arg_1D69_0, arg_1D69_1, arg_1D69_2, arg_1D69_3, arg_1D69_4, arg_1D69_5, arg_1D69_6, newColor, 0.1f);
											Dust expr_1D7D_cp_0 = Main.dust[num35];
											expr_1D7D_cp_0.velocity.X = expr_1D7D_cp_0.velocity.X / 2f;
											Dust expr_1D9B_cp_0 = Main.dust[num35];
											expr_1D9B_cp_0.velocity.Y = expr_1D9B_cp_0.velocity.Y / 2f;
											Main.dust[num35].noGravity = true;
											Main.dust[num35].fadeIn = 1f;
										}
										if (num34 == 1 && Main.rand.Next(100) == 0)
										{
											Vector2 arg_1E14_0 = new Vector2((float)(j * 16), (float)(i * 16));
											int arg_1E14_1 = 16;
											int arg_1E14_2 = 16;
											int arg_1E14_3 = 41;
											float arg_1E14_4 = 0f;
											float arg_1E14_5 = 0f;
											int arg_1E14_6 = 250;
											Color newColor = default(Color);
											Dust.NewDust(arg_1E14_0, arg_1E14_1, arg_1E14_2, arg_1E14_3, arg_1E14_4, arg_1E14_5, arg_1E14_6, newColor, 0.8f);
										}
										if (num34 == 3)
										{
											if (Main.rand.Next(200) == 0)
											{
												Vector2 arg_1E65_0 = new Vector2((float)(j * 16), (float)(i * 16));
												int arg_1E65_1 = 16;
												int arg_1E65_2 = 16;
												int arg_1E65_3 = 14;
												float arg_1E65_4 = 0f;
												float arg_1E65_5 = 0f;
												int arg_1E65_6 = 100;
												Color newColor = default(Color);
												int num36 = Dust.NewDust(arg_1E65_0, arg_1E65_1, arg_1E65_2, arg_1E65_3, arg_1E65_4, arg_1E65_5, arg_1E65_6, newColor, 0.2f);
												Main.dust[num36].fadeIn = 1.2f;
											}
											if (Main.rand.Next(75) == 0)
											{
												Vector2 arg_1EBE_0 = new Vector2((float)(j * 16), (float)(i * 16));
												int arg_1EBE_1 = 16;
												int arg_1EBE_2 = 16;
												int arg_1EBE_3 = 27;
												float arg_1EBE_4 = 0f;
												float arg_1EBE_5 = 0f;
												int arg_1EBE_6 = 100;
												Color newColor = default(Color);
												int num37 = Dust.NewDust(arg_1EBE_0, arg_1EBE_1, arg_1EBE_2, arg_1EBE_3, arg_1EBE_4, arg_1EBE_5, arg_1EBE_6, newColor, 1f);
												Dust expr_1ED2_cp_0 = Main.dust[num37];
												expr_1ED2_cp_0.velocity.X = expr_1ED2_cp_0.velocity.X / 2f;
												Dust expr_1EF0_cp_0 = Main.dust[num37];
												expr_1EF0_cp_0.velocity.Y = expr_1EF0_cp_0.velocity.Y / 2f;
											}
										}
										if (num34 == 4 && Main.rand.Next(150) == 0)
										{
											Vector2 arg_1F4D_0 = new Vector2((float)(j * 16), (float)(i * 16));
											int arg_1F4D_1 = 16;
											int arg_1F4D_2 = 8;
											int arg_1F4D_3 = 16;
											float arg_1F4D_4 = 0f;
											float arg_1F4D_5 = 0f;
											int arg_1F4D_6 = 0;
											Color newColor = default(Color);
											int num38 = Dust.NewDust(arg_1F4D_0, arg_1F4D_1, arg_1F4D_2, arg_1F4D_3, arg_1F4D_4, arg_1F4D_5, arg_1F4D_6, newColor, 1f);
											Dust expr_1F61_cp_0 = Main.dust[num38];
											expr_1F61_cp_0.velocity.X = expr_1F61_cp_0.velocity.X / 3f;
											Dust expr_1F7F_cp_0 = Main.dust[num38];
											expr_1F7F_cp_0.velocity.Y = expr_1F7F_cp_0.velocity.Y / 3f;
											Dust expr_1F9D_cp_0 = Main.dust[num38];
											expr_1F9D_cp_0.velocity.Y = expr_1F9D_cp_0.velocity.Y - 0.7f;
											Main.dust[num38].alpha = 50;
											Main.dust[num38].scale *= 0.1f;
											Main.dust[num38].fadeIn = 0.9f;
											Main.dust[num38].noGravity = true;
										}
										if (num34 == 5)
										{
											if (Main.rand.Next(40) == 0)
											{
												Vector2 arg_203E_0 = new Vector2((float)(j * 16), (float)(i * 16 - 6));
												int arg_203E_1 = 16;
												int arg_203E_2 = 16;
												int arg_203E_3 = 6;
												float arg_203E_4 = 0f;
												float arg_203E_5 = 0f;
												int arg_203E_6 = 0;
												Color newColor = default(Color);
												int num39 = Dust.NewDust(arg_203E_0, arg_203E_1, arg_203E_2, arg_203E_3, arg_203E_4, arg_203E_5, arg_203E_6, newColor, 1.5f);
												Dust expr_2052_cp_0 = Main.dust[num39];
												expr_2052_cp_0.velocity.Y = expr_2052_cp_0.velocity.Y - 2f;
												Main.dust[num39].noGravity = true;
											}
                                            color.A = (byte)(Main.mouseTextColor / 2);
											color.G = Main.mouseTextColor;
											color.B = Main.mouseTextColor;
										}
									}
									SpriteBatch arg_2132_0 = this.spriteBatch;
									Texture2D arg_2132_1 = Main.tileTexture[num33];
									Vector2 arg_2132_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num5 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num6));
									Rectangle? arg_2132_3 = new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num5, height));
									Color arg_2132_4 = color;
									float arg_2132_5 = 0f;
									Vector2 origin = default(Vector2);
									arg_2132_0.Draw(arg_2132_1, arg_2132_2, arg_2132_3, arg_2132_4, arg_2132_5, origin, 1f, SpriteEffects.None, 0f);
								}
								else
								{
									SpriteBatch arg_21E7_0 = this.spriteBatch;
									Texture2D arg_21E7_1 = Main.tileTexture[(int)Main.tile[j, i].type];
									Vector2 arg_21E7_2 = new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num5 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.Y + num6));
									Rectangle? arg_21E7_3 = new Rectangle?(new Rectangle((int)Main.tile[j, i].frameX, (int)Main.tile[j, i].frameY, num5, height));
									Color arg_21E7_4 = color;
									float arg_21E7_5 = 0f;
									Vector2 origin = default(Vector2);
									arg_21E7_0.Draw(arg_21E7_1, arg_21E7_2, arg_21E7_3, arg_21E7_4, arg_21E7_5, origin, 1f, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
			}
		}
		protected void DrawWater(bool bg = false)
		{
			int num = (int)(Main.screenPosition.X / 16f - 1f);
			int num2 = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			int num3 = (int)(Main.screenPosition.Y / 16f - 1f);
			int num4 = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num3; i < num4 + 4; i++)
			{
				for (int j = num - 2; j < num2 + 2; j++)
				{
					if (Main.tile[j, i].liquid > 0 && Lighting.Brightness(j, i) > 0f)
					{
						Color color = Lighting.GetColor(j, i);
						float num5 = (float)(256 - (int)Main.tile[j, i].liquid);
						num5 /= 32f;
						int num6 = 0;
						if (Main.tile[j, i].lava)
						{
							num6 = 1;
						}
						float num7 = 0.5f;
						if (bg)
						{
							num7 = 1f;
						}
						Vector2 value = new Vector2((float)(j * 16), (float)(i * 16 + (int)num5 * 2));
						Rectangle value2 = new Rectangle(0, 0, 16, 16 - (int)num5 * 2);
						if (Main.tile[j, i + 1].liquid < 245 && (!Main.tile[j, i + 1].active || !Main.tileSolid[(int)Main.tile[j, i + 1].type] || Main.tileSolidTop[(int)Main.tile[j, i + 1].type]))
						{
							float num8 = (float)(256 - (int)Main.tile[j, i + 1].liquid);
							num8 /= 32f;
							num7 = 0.5f * (8f - num5) / 4f;
							if ((double)num7 > 0.55)
							{
								num7 = 0.55f;
							}
							if ((double)num7 < 0.35)
							{
								num7 = 0.35f;
							}
							float num9 = num5 / 2f;
							if (Main.tile[j, i + 1].liquid < 200)
							{
								if (bg)
								{
									goto IL_774;
								}
								if (Main.tile[j, i - 1].liquid > 0 && Main.tile[j, i - 1].liquid > 0)
								{
									value2 = new Rectangle(0, 4, 16, 16);
									num7 = 0.5f;
								}
								else
								{
									if (Main.tile[j, i - 1].liquid > 0)
									{
										value = new Vector2((float)(j * 16), (float)(i * 16 + 4));
										value2 = new Rectangle(0, 4, 16, 12);
										num7 = 0.5f;
									}
									else
									{
										if (Main.tile[j, i + 1].liquid > 0)
										{
											value = new Vector2((float)(j * 16), (float)(i * 16 + (int)num5 * 2 + (int)num8 * 2));
											value2 = new Rectangle(0, 4, 16, 16 - (int)num5 * 2);
										}
										else
										{
											value = new Vector2((float)(j * 16 + (int)num9), (float)(i * 16 + (int)num9 * 2 + (int)num8 * 2));
											value2 = new Rectangle(0, 4, 16 - (int)num9 * 2, 16 - (int)num9 * 2);
										}
									}
								}
							}
							else
							{
								num7 = 0.5f;
								value2 = new Rectangle(0, 4, 16, 16 - (int)num5 * 2 + (int)num8 * 2);
							}
						}
						else
						{
							if (Main.tile[j, i - 1].liquid > 32)
							{
								value2 = new Rectangle(0, 4, value2.Width, value2.Height);
							}
							else
							{
								if (num5 < 1f && Main.tile[j, i - 1].active && Main.tileSolid[(int)Main.tile[j, i - 1].type] && !Main.tileSolidTop[(int)Main.tile[j, i - 1].type])
								{
									value = new Vector2((float)(j * 16), (float)(i * 16));
									value2 = new Rectangle(0, 4, 16, 16);
								}
								else
								{
									bool flag = true;
									int num10 = i + 1;
									while (num10 < i + 6 && (!Main.tile[j, num10].active || !Main.tileSolid[(int)Main.tile[j, num10].type] || Main.tileSolidTop[(int)Main.tile[j, num10].type]))
									{
										if (Main.tile[j, num10].liquid < 200)
										{
											flag = false;
											break;
										}
										num10++;
									}
									if (!flag)
									{
										num7 = 0.5f;
										value2 = new Rectangle(0, 4, 16, 16);
									}
									else
									{
										if (Main.tile[j, i - 1].liquid > 0)
										{
											value2 = new Rectangle(0, 2, value2.Width, value2.Height);
										}
									}
								}
							}
						}
						if (Main.tile[j, i].lava)
						{
							num7 *= 1.8f;
							if (num7 > 1f)
							{
								num7 = 1f;
							}
							if (base.IsActive && !Main.gamePaused && Dust.lavaBubbles < 200)
							{
								if (Main.tile[j, i].liquid > 200 && Main.rand.Next(700) == 0)
								{
									Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 35, 0f, 0f, 0, default(Color), 1f);
								}
								if (value2.Y == 0 && Main.rand.Next(350) == 0)
								{
									int num11 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16) + num5 * 2f - 8f), 16, 8, 35, 0f, 0f, 50, default(Color), 1.5f);
									Dust expr_64A = Main.dust[num11];
									expr_64A.velocity *= 0.8f;
									Dust expr_66C_cp_0 = Main.dust[num11];
									expr_66C_cp_0.velocity.X = expr_66C_cp_0.velocity.X * 2f;
									Dust expr_68A_cp_0 = Main.dust[num11];
									expr_68A_cp_0.velocity.Y = expr_68A_cp_0.velocity.Y - (float)Main.rand.Next(1, 7) * 0.1f;
									if (Main.rand.Next(10) == 0)
									{
										Dust expr_6C4_cp_0 = Main.dust[num11];
										expr_6C4_cp_0.velocity.Y = expr_6C4_cp_0.velocity.Y * (float)Main.rand.Next(2, 5);
									}
									Main.dust[num11].noGravity = true;
								}
							}
						}
						float num12 = (float)color.R * num7;
						float num13 = (float)color.G * num7;
						float num14 = (float)color.B * num7;
						float num15 = (float)color.A * num7;
						color = new Color((int)((byte)num12), (int)((byte)num13), (int)((byte)num14), (int)((byte)num15));
						this.spriteBatch.Draw(Main.liquidTexture[num6], value - Main.screenPosition, new Rectangle?(value2), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					IL_774:;
				}
			}
		}
		protected void DrawGore()
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.gore[i].active && Main.gore[i].type > 0)
				{
					Color alpha = Main.gore[i].GetAlpha(Lighting.GetColor((int)((double)Main.gore[i].position.X + (double)Main.goreTexture[Main.gore[i].type].Width * 0.5) / 16, (int)(((double)Main.gore[i].position.Y + (double)Main.goreTexture[Main.gore[i].type].Height * 0.5) / 16.0)));
					this.spriteBatch.Draw(Main.goreTexture[Main.gore[i].type], new Vector2(Main.gore[i].position.X - Main.screenPosition.X + (float)(Main.goreTexture[Main.gore[i].type].Width / 2), Main.gore[i].position.Y - Main.screenPosition.Y + (float)(Main.goreTexture[Main.gore[i].type].Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.goreTexture[Main.gore[i].type].Width, Main.goreTexture[Main.gore[i].type].Height)), alpha, Main.gore[i].rotation, new Vector2((float)(Main.goreTexture[Main.gore[i].type].Width / 2), (float)(Main.goreTexture[Main.gore[i].type].Height / 2)), Main.gore[i].scale, SpriteEffects.None, 0f);
				}
			}
		}
		protected void DrawNPCs(bool behindTiles = false)
		{
			Rectangle rectangle = new Rectangle((int)Main.screenPosition.X - 300, (int)Main.screenPosition.Y - 300, Main.screenWidth + 600, Main.screenHeight + 600);
			for (int i = 999; i >= 0; i--)
			{
				if (Main.npc[i].active && Main.npc[i].type > 0 && Main.npc[i].behindTiles == behindTiles && rectangle.Intersects(new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height)))
				{
					if (Main.npc[i].aiStyle == 13)
					{
						Vector2 vector = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y + (float)(Main.npc[i].height / 2));
						float num = Main.npc[i].ai[0] * 16f + 8f - vector.X;
						float num2 = Main.npc[i].ai[1] * 16f + 8f - vector.Y;
						float rotation = (float)Math.Atan2((double)num2, (double)num) - 1.57f;
						bool flag = true;
						while (flag)
						{
							int height = 28;
							float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
							if (num3 < 40f)
							{
								height = (int)num3 - 40 + 28;
								flag = false;
							}
							num3 = 28f / num3;
							num *= num3;
							num2 *= num3;
							vector.X += num;
							vector.Y += num2;
							num = Main.npc[i].ai[0] * 16f + 8f - vector.X;
							num2 = Main.npc[i].ai[1] * 16f + 8f - vector.Y;
							Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
							if (Main.npc[i].type == 56)
							{
								this.spriteBatch.Draw(Main.chain5Texture, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height)), color, rotation, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							}
							else
							{
								this.spriteBatch.Draw(Main.chain4Texture, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height)), color, rotation, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							}
						}
					}
					if (Main.npc[i].type == 36)
					{
						Vector2 vector2 = new Vector2(Main.npc[i].position.X + (float)Main.npc[i].width * 0.5f - 5f * Main.npc[i].ai[0], Main.npc[i].position.Y + 20f);
						for (int j = 0; j < 2; j++)
						{
							float num4 = Main.npc[(int)Main.npc[i].ai[1]].position.X + (float)(Main.npc[(int)Main.npc[i].ai[1]].width / 2) - vector2.X;
							float num5 = Main.npc[(int)Main.npc[i].ai[1]].position.Y + (float)(Main.npc[(int)Main.npc[i].ai[1]].height / 2) - vector2.Y;
							float num6 = 0f;
							if (j == 0)
							{
								num4 -= 200f * Main.npc[i].ai[0];
								num5 += 130f;
								num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
								num6 = 92f / num6;
								vector2.X += num4 * num6;
								vector2.Y += num5 * num6;
							}
							else
							{
								num4 -= 50f * Main.npc[i].ai[0];
								num5 += 80f;
								num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
								num6 = 60f / num6;
								vector2.X += num4 * num6;
								vector2.Y += num5 * num6;
							}
							float rotation2 = (float)Math.Atan2((double)num5, (double)num4) - 1.57f;
							Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
							this.spriteBatch.Draw(Main.boneArmTexture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), color2, rotation2, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							if (j == 0)
							{
								vector2.X += num4 * num6 / 2f;
								vector2.Y += num5 * num6 / 2f;
							}
							else
							{
								if (base.IsActive)
								{
									vector2.X += num4 * num6 - 16f;
									vector2.Y += num5 * num6 - 6f;
									int num7 = Dust.NewDust(new Vector2(vector2.X, vector2.Y), 30, 10, 5, num4 * 0.02f, num5 * 0.02f, 0, default(Color), 2f);
									Main.dust[num7].noGravity = true;
								}
							}
						}
					}
					if (Main.npc[i].aiStyle == 20)
					{
						Vector2 vector3 = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y + (float)(Main.npc[i].height / 2));
						float num8 = Main.npc[i].ai[1] - vector3.X;
						float num9 = Main.npc[i].ai[2] - vector3.Y;
						float num10 = (float)Math.Atan2((double)num9, (double)num8) - 1.57f;
						Main.npc[i].rotation = num10;
						bool flag2 = true;
						while (flag2)
						{
							int height2 = 12;
							float num11 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
							if (num11 < 20f)
							{
								height2 = (int)num11 - 20 + 12;
								flag2 = false;
							}
							num11 = 12f / num11;
							num8 *= num11;
							num9 *= num11;
							vector3.X += num8;
							vector3.Y += num9;
							num8 = Main.npc[i].ai[1] - vector3.X;
							num9 = Main.npc[i].ai[2] - vector3.Y;
							Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
							this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector3.X - Main.screenPosition.X, vector3.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, height2)), color3, num10, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
						}
						this.spriteBatch.Draw(Main.spikeBaseTexture, new Vector2(Main.npc[i].ai[1] - Main.screenPosition.X, Main.npc[i].ai[2] - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.spikeBaseTexture.Width, Main.spikeBaseTexture.Height)), Lighting.GetColor((int)Main.npc[i].ai[1] / 16, (int)(Main.npc[i].ai[2] / 16f)), num10 - 0.75f, new Vector2((float)Main.spikeBaseTexture.Width * 0.5f, (float)Main.spikeBaseTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
					Color color4 = Lighting.GetColor((int)((double)Main.npc[i].position.X + (double)Main.npc[i].width * 0.5) / 16, (int)(((double)Main.npc[i].position.Y + (double)Main.npc[i].height * 0.5) / 16.0));
					float num12 = 1f;
					float num13 = 1f;
					float num14 = 1f;
					float num15 = 1f;
					if (Main.npc[i].poisoned)
					{
						if (Main.rand.Next(30) == 0)
						{
							int num16 = Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 46, 0f, 0f, 120, default(Color), 0.2f);
							Main.dust[num16].noGravity = true;
							Main.dust[num16].fadeIn = 1.9f;
						}
						num12 *= 0.65f;
						num14 *= 0.75f;
					}
					if (Main.npc[i].onFire)
					{
						if (Main.rand.Next(4) < 3)
						{
							int num17 = Dust.NewDust(new Vector2(Main.npc[i].position.X - 2f, Main.npc[i].position.Y - 2f), Main.npc[i].width + 4, Main.npc[i].height + 4, 6, Main.npc[i].velocity.X * 0.4f, Main.npc[i].velocity.Y * 0.4f, 100, default(Color), 2.5f);
							Main.dust[num17].noGravity = true;
							Dust expr_BA6 = Main.dust[num17];
							expr_BA6.velocity *= 1.8f;
							Dust expr_BC8_cp_0 = Main.dust[num17];
							expr_BC8_cp_0.velocity.Y = expr_BC8_cp_0.velocity.Y - 0.5f;
						}
						num14 *= 0.6f;
						num13 *= 0.7f;
					}
					if (num12 != 1f || num13 != 1f || num14 != 1f || num15 != 1f)
					{
						color4 = Main.buffColor(color4, num12, num13, num14, num15);
					}
					if (Main.player[Main.myPlayer].detectCreature && Main.npc[i].lifeMax > 1)
					{
						if (color4.R < 150)
						{
							color4.A = Main.mouseTextColor;
						}
						if (color4.R < 50)
						{
							color4.R = 50;
						}
						if (color4.G < 200)
						{
							color4.G = 200;
						}
						if (color4.B < 100)
						{
							color4.B = 100;
						}
						if (!Main.gamePaused && base.IsActive && Main.rand.Next(50) == 0)
						{
							int num18 = Dust.NewDust(new Vector2(Main.npc[i].position.X, Main.npc[i].position.Y), Main.npc[i].width, Main.npc[i].height, 15, 0f, 0f, 150, default(Color), 0.8f);
							Dust expr_D39 = Main.dust[num18];
							expr_D39.velocity *= 0.1f;
							Main.dust[num18].noLight = true;
						}
					}
					if (Main.npc[i].type == 50)
					{
						Vector2 vector4 = default(Vector2);
						float num19 = 0f;
						vector4.Y -= Main.npc[i].velocity.Y;
						vector4.X -= Main.npc[i].velocity.X * 2f;
						num19 += Main.npc[i].velocity.X * 0.05f;
						if (Main.npc[i].frame.Y == 120)
						{
							vector4.Y += 2f;
						}
						if (Main.npc[i].frame.Y == 360)
						{
							vector4.Y -= 2f;
						}
						if (Main.npc[i].frame.Y == 480)
						{
							vector4.Y -= 6f;
						}
						this.spriteBatch.Draw(Main.ninjaTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) + vector4.X, Main.npc[i].position.Y - Main.screenPosition.Y + (float)(Main.npc[i].height / 2) + vector4.Y), new Rectangle?(new Rectangle(0, 0, Main.ninjaTexture.Width, Main.ninjaTexture.Height)), color4, num19, new Vector2((float)(Main.ninjaTexture.Width / 2), (float)(Main.ninjaTexture.Height / 2)), 1f, SpriteEffects.None, 0f);
					}
					if (Main.npc[i].type == 71)
					{
						Vector2 vector5 = default(Vector2);
						float num20 = 0f;
						vector5.Y -= Main.npc[i].velocity.Y * 0.3f;
						vector5.X -= Main.npc[i].velocity.X * 0.6f;
						num20 += Main.npc[i].velocity.X * 0.09f;
						if (Main.npc[i].frame.Y == 120)
						{
							vector5.Y += 2f;
						}
						if (Main.npc[i].frame.Y == 360)
						{
							vector5.Y -= 2f;
						}
						if (Main.npc[i].frame.Y == 480)
						{
							vector5.Y -= 6f;
						}
						this.spriteBatch.Draw(Main.itemTexture[327], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) + vector5.X, Main.npc[i].position.Y - Main.screenPosition.Y + (float)(Main.npc[i].height / 2) + vector5.Y), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[327].Width, Main.itemTexture[327].Height)), color4, num20, new Vector2((float)(Main.itemTexture[327].Width / 2), (float)(Main.itemTexture[327].Height / 2)), 1f, SpriteEffects.None, 0f);
					}
					if (Main.npc[i].type == 69)
					{
						this.spriteBatch.Draw(Main.antLionTexture, new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height + 14f), new Rectangle?(new Rectangle(0, 0, Main.antLionTexture.Width, Main.antLionTexture.Height)), color4, -Main.npc[i].rotation * 0.3f, new Vector2((float)(Main.antLionTexture.Width / 2), (float)(Main.antLionTexture.Height / 2)), 1f, SpriteEffects.None, 0f);
					}
					float num21 = 0f;
					Vector2 origin = new Vector2((float)(Main.npcTexture[Main.npc[i].type].Width / 2), (float)(Main.npcTexture[Main.npc[i].type].Height / Main.npcFrameCount[Main.npc[i].type] / 2));
					if (Main.npc[i].type == 4)
					{
						origin = new Vector2(55f, 107f);
					}
					else
					{
						if (Main.npc[i].type == 6)
						{
							num21 = 26f;
						}
						else
						{
							if (Main.npc[i].type == 7 || Main.npc[i].type == 8 || Main.npc[i].type == 9)
							{
								num21 = 13f;
							}
							else
							{
								if (Main.npc[i].type == 10 || Main.npc[i].type == 11 || Main.npc[i].type == 12)
								{
									num21 = 8f;
								}
								else
								{
									if (Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15)
									{
										num21 = 26f;
									}
									else
									{
										if (Main.npc[i].type == 48)
										{
											num21 = 32f;
										}
										else
										{
											if (Main.npc[i].type == 49 || Main.npc[i].type == 51)
											{
												num21 = 4f;
											}
											else
											{
												if (Main.npc[i].type == 60)
												{
													num21 = 10f;
												}
												else
												{
													if (Main.npc[i].type == 62 || Main.npc[i].type == 66)
													{
														num21 = 14f;
													}
													else
													{
														if (Main.npc[i].type == 63 || Main.npc[i].type == 64)
														{
															num21 = 4f;
														}
														else
														{
															if (Main.npc[i].type == 65)
															{
																num21 = 14f;
															}
															else
															{
																if (Main.npc[i].type == 69)
																{
																	num21 = 4f;
																	origin.Y += 8f;
																}
																else
																{
																	if (Main.npc[i].type == 70)
																	{
																		num21 = -4f;
																	}
																	else
																	{
																		if (Main.npc[i].type == 72)
																		{
																			num21 = -2f;
																		}
																		else
																		{
																			if (Main.npc[i].type == 39 || Main.npc[i].type == 40 || Main.npc[i].type == 41)
																			{
																				num21 = 26f;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					num21 *= Main.npc[i].scale;
					if (Main.npc[i].aiStyle == 10 || Main.npc[i].type == 72)
					{
						color4 = Color.White;
					}
					SpriteEffects effects = SpriteEffects.None;
					if (Main.npc[i].spriteDirection == 1)
					{
						effects = SpriteEffects.FlipHorizontally;
					}
					this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num21), new Rectangle?(Main.npc[i].frame), Main.npc[i].GetAlpha(color4), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
					if (Main.npc[i].color != default(Color))
					{
						this.spriteBatch.Draw(Main.npcTexture[Main.npc[i].type], new Vector2(Main.npc[i].position.X - Main.screenPosition.X + (float)(Main.npc[i].width / 2) - (float)Main.npcTexture[Main.npc[i].type].Width * Main.npc[i].scale / 2f + origin.X * Main.npc[i].scale, Main.npc[i].position.Y - Main.screenPosition.Y + (float)Main.npc[i].height - (float)Main.npcTexture[Main.npc[i].type].Height * Main.npc[i].scale / (float)Main.npcFrameCount[Main.npc[i].type] + 4f + origin.Y * Main.npc[i].scale + num21), new Rectangle?(Main.npc[i].frame), Main.npc[i].GetColor(color4), Main.npc[i].rotation, origin, Main.npc[i].scale, effects, 0f);
					}
				}
			}
		}
		protected void DrawProj(int i)
		{
			if (Main.projectile[i].type == 32)
			{
				Vector2 vector = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
				float num = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector.X;
				float num2 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector.Y;
				float rotation = (float)Math.Atan2((double)num2, (double)num) - 1.57f;
				bool flag = true;
				if (num == 0f && num2 == 0f)
				{
					flag = false;
				}
				else
				{
					float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
					num3 = 8f / num3;
					num *= num3;
					num2 *= num3;
					vector.X -= num;
					vector.Y -= num2;
					num = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector.X;
					num2 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector.Y;
				}
				while (flag)
				{
					float num4 = (float)Math.Sqrt((double)(num * num + num2 * num2));
					if (num4 < 28f)
					{
						flag = false;
					}
					else
					{
						num4 = 28f / num4;
						num *= num4;
						num2 *= num4;
						vector.X += num;
						vector.Y += num2;
						num = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector.X;
						num2 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector.Y;
						Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
						this.spriteBatch.Draw(Main.chain5Texture, new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain5Texture.Width, Main.chain5Texture.Height)), color, rotation, new Vector2((float)Main.chain5Texture.Width * 0.5f, (float)Main.chain5Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			else
			{
				if (Main.projectile[i].aiStyle == 7)
				{
					Vector2 vector2 = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
					float num5 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector2.X;
					float num6 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector2.Y;
					float rotation2 = (float)Math.Atan2((double)num6, (double)num5) - 1.57f;
					bool flag2 = true;
					while (flag2)
					{
						float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
						if (num7 < 25f)
						{
							flag2 = false;
						}
						else
						{
							num7 = 12f / num7;
							num5 *= num7;
							num6 *= num7;
							vector2.X += num5;
							vector2.Y += num6;
							num5 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector2.X;
							num6 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector2.Y;
							Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
							this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, Main.chainTexture.Height)), color2, rotation2, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
						}
					}
				}
				else
				{
					if (Main.projectile[i].aiStyle == 13)
					{
						float num8 = Main.projectile[i].position.X + 8f;
						float num9 = Main.projectile[i].position.Y + 2f;
						float num10 = Main.projectile[i].velocity.X;
						float num11 = Main.projectile[i].velocity.Y;
						float num12 = (float)Math.Sqrt((double)(num10 * num10 + num11 * num11));
						num12 = 20f / num12;
						if (Main.projectile[i].ai[0] == 0f)
						{
							num8 -= Main.projectile[i].velocity.X * num12;
							num9 -= Main.projectile[i].velocity.Y * num12;
						}
						else
						{
							num8 += Main.projectile[i].velocity.X * num12;
							num9 += Main.projectile[i].velocity.Y * num12;
						}
						Vector2 vector3 = new Vector2(num8, num9);
						num10 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector3.X;
						num11 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector3.Y;
						float rotation3 = (float)Math.Atan2((double)num11, (double)num10) - 1.57f;
						if (Main.projectile[i].alpha == 0)
						{
							int num13 = -1;
							if (Main.projectile[i].position.X + (float)(Main.projectile[i].width / 2) < Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2))
							{
								num13 = 1;
							}
							if (Main.player[Main.projectile[i].owner].direction == 1)
							{
								Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num11 * (float)num13), (double)(num10 * (float)num13));
							}
							else
							{
								Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num11 * (float)num13), (double)(num10 * (float)num13));
							}
						}
						bool flag3 = true;
						while (flag3)
						{
							float num14 = (float)Math.Sqrt((double)(num10 * num10 + num11 * num11));
							if (num14 < 25f)
							{
								flag3 = false;
							}
							else
							{
								num14 = 12f / num14;
								num10 *= num14;
								num11 *= num14;
								vector3.X += num10;
								vector3.Y += num11;
								num10 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector3.X;
								num11 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector3.Y;
								Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
								this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector3.X - Main.screenPosition.X, vector3.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, Main.chainTexture.Height)), color3, rotation3, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
							}
						}
					}
					else
					{
						if (Main.projectile[i].aiStyle == 15)
						{
							Vector2 vector4 = new Vector2(Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f, Main.projectile[i].position.Y + (float)Main.projectile[i].height * 0.5f);
							float num15 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector4.X;
							float num16 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector4.Y;
							float rotation4 = (float)Math.Atan2((double)num16, (double)num15) - 1.57f;
							if (Main.projectile[i].alpha == 0)
							{
								int num17 = -1;
								if (Main.projectile[i].position.X + (float)(Main.projectile[i].width / 2) < Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2))
								{
									num17 = 1;
								}
								if (Main.player[Main.projectile[i].owner].direction == 1)
								{
									Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num16 * (float)num17), (double)(num15 * (float)num17));
								}
								else
								{
									Main.player[Main.projectile[i].owner].itemRotation = (float)Math.Atan2((double)(num16 * (float)num17), (double)(num15 * (float)num17));
								}
							}
							bool flag4 = true;
							while (flag4)
							{
								float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
								if (num18 < 25f)
								{
									flag4 = false;
								}
								else
								{
									num18 = 12f / num18;
									num15 *= num18;
									num16 *= num18;
									vector4.X += num15;
									vector4.Y += num16;
									num15 = Main.player[Main.projectile[i].owner].position.X + (float)(Main.player[Main.projectile[i].owner].width / 2) - vector4.X;
									num16 = Main.player[Main.projectile[i].owner].position.Y + (float)(Main.player[Main.projectile[i].owner].height / 2) - vector4.Y;
									Color color4 = Lighting.GetColor((int)vector4.X / 16, (int)(vector4.Y / 16f));
									if (Main.projectile[i].type == 25)
									{
										this.spriteBatch.Draw(Main.chain2Texture, new Vector2(vector4.X - Main.screenPosition.X, vector4.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain2Texture.Width, Main.chain2Texture.Height)), color4, rotation4, new Vector2((float)Main.chain2Texture.Width * 0.5f, (float)Main.chain2Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
									}
									else
									{
										if (Main.projectile[i].type == 35)
										{
											this.spriteBatch.Draw(Main.chain6Texture, new Vector2(vector4.X - Main.screenPosition.X, vector4.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain6Texture.Width, Main.chain6Texture.Height)), color4, rotation4, new Vector2((float)Main.chain6Texture.Width * 0.5f, (float)Main.chain6Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
										}
										else
										{
											this.spriteBatch.Draw(Main.chain3Texture, new Vector2(vector4.X - Main.screenPosition.X, vector4.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain3Texture.Width, Main.chain3Texture.Height)), color4, rotation4, new Vector2((float)Main.chain3Texture.Width * 0.5f, (float)Main.chain3Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
										}
									}
								}
							}
						}
					}
				}
			}
			Color newColor = Lighting.GetColor((int)((double)Main.projectile[i].position.X + (double)Main.projectile[i].width * 0.5) / 16, (int)(((double)Main.projectile[i].position.Y + (double)Main.projectile[i].height * 0.5) / 16.0));
			if (Main.projectile[i].hide)
			{
				newColor = Lighting.GetColor((int)((double)Main.player[Main.projectile[i].owner].position.X + (double)Main.player[Main.projectile[i].owner].width * 0.5) / 16, (int)(((double)Main.player[Main.projectile[i].owner].position.Y + (double)Main.player[Main.projectile[i].owner].height * 0.5) / 16.0));
			}
			if (Main.projectile[i].type == 14)
			{
				newColor = Color.White;
			}
			int num19 = 0;
			int num20 = 0;
			if (Main.projectile[i].type == 16)
			{
				num19 = 6;
			}
			if (Main.projectile[i].type == 17 || Main.projectile[i].type == 31)
			{
				num19 = 2;
			}
			if (Main.projectile[i].type == 25 || Main.projectile[i].type == 26 || Main.projectile[i].type == 35)
			{
				num19 = 6;
				num20 -= 6;
			}
			if (Main.projectile[i].type == 28 || Main.projectile[i].type == 37)
			{
				num19 = 8;
			}
			if (Main.projectile[i].type == 29)
			{
				num19 = 11;
			}
			if (Main.projectile[i].type == 43)
			{
				num19 = 4;
			}
			float num21 = (float)(Main.projectileTexture[Main.projectile[i].type].Width - Main.projectile[i].width) * 0.5f + (float)Main.projectile[i].width * 0.5f;
			if (Main.projectile[i].type == 50 || Main.projectile[i].type == 53)
			{
				num20 = -8;
			}
			if (Main.projectile[i].aiStyle == 19)
			{
				this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + (float)(Main.projectile[i].width / 2), Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[i].type].Width, Main.projectileTexture[Main.projectile[i].type].Height)), Main.projectile[i].GetAlpha(newColor), Main.projectile[i].rotation, default(Vector2), Main.projectile[i].scale, SpriteEffects.None, 0f);
				return;
			}
			this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[i].type], new Vector2(Main.projectile[i].position.X - Main.screenPosition.X + num21 + (float)num20, Main.projectile[i].position.Y - Main.screenPosition.Y + (float)(Main.projectile[i].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[i].type].Width, Main.projectileTexture[Main.projectile[i].type].Height)), Main.projectile[i].GetAlpha(newColor), Main.projectile[i].rotation, new Vector2(num21, (float)(Main.projectile[i].height / 2 + num19)), Main.projectile[i].scale, SpriteEffects.None, 0f);
		}
		private static Color buffColor(Color newColor, float R, float G, float B, float A)
		{
			newColor.R = (byte)((float)newColor.R * R);
			newColor.G = (byte)((float)newColor.G * G);
			newColor.B = (byte)((float)newColor.B * B);
			newColor.A = (byte)((float)newColor.A * A);
			return newColor;
		}
		protected void DrawGhost(Player drawPlayer)
		{
			SpriteEffects effects = SpriteEffects.None;
			if (drawPlayer.direction == 1)
			{
				effects = SpriteEffects.None;
			}
			else
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			Color immuneAlpha = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, new Color((int)(Main.mouseTextColor / 2 + 100), (int)(Main.mouseTextColor / 2 + 100), (int)(Main.mouseTextColor / 2 + 100), (int)(Main.mouseTextColor / 2 + 100))));
			Rectangle value = new Rectangle(0, Main.ghostTexture.Height / 4 * drawPlayer.ghostFrame, Main.ghostTexture.Width, Main.ghostTexture.Height / 4);
			Vector2 origin = new Vector2((float)value.Width * 0.5f, (float)value.Height * 0.5f);
			this.spriteBatch.Draw(Main.ghostTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X + (float)(value.Width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)(value.Height / 2)))), new Rectangle?(value), immuneAlpha, 0f, origin, 1f, effects, 0f);
		}
		protected void DrawPlayer(Player drawPlayer)
		{
			SpriteEffects effects = SpriteEffects.None;
			SpriteEffects effects2 = SpriteEffects.FlipHorizontally;
			Color color = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White));
			Color color2 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.eyeColor));
			Color color3 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.hairColor));
			Color color4 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.skinColor));
			Color color5 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.skinColor));
			Color color6 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.shirtColor));
			Color color7 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.underShirtColor));
			Color color8 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.pantsColor));
			Color color9 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.shoeColor));
			Color color10 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16, Color.White));
			Color color11 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, Color.White));
			Color color12 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16, Color.White));
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			float num4 = 1f;
			if (drawPlayer.poisoned)
			{
				if (Main.rand.Next(50) == 0)
				{
					int num5 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 46, 0f, 0f, 150, default(Color), 0.2f);
					Main.dust[num5].noGravity = true;
					Main.dust[num5].fadeIn = 1.9f;
				}
				num *= 0.65f;
				num3 *= 0.75f;
			}
			if (drawPlayer.onFire)
			{
				if (Main.rand.Next(4) == 0)
				{
					int num6 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 6, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 2.5f);
					Main.dust[num6].noGravity = true;
					Dust expr_593 = Main.dust[num6];
					expr_593.velocity *= 1.8f;
					Dust expr_5B5_cp_0 = Main.dust[num6];
					expr_5B5_cp_0.velocity.Y = expr_5B5_cp_0.velocity.Y - 0.5f;
				}
				num3 *= 0.6f;
				num2 *= 0.7f;
			}
			if (drawPlayer.noItems)
			{
				num2 *= 0.8f;
				num *= 0.65f;
			}
			if (drawPlayer.blind)
			{
				num2 *= 0.65f;
				num *= 0.7f;
			}
			if (num != 1f || num2 != 1f || num3 != 1f || num4 != 1f)
			{
				color = Main.buffColor(color, num, num2, num3, num4);
				color2 = Main.buffColor(color2, num, num2, num3, num4);
				color3 = Main.buffColor(color3, num, num2, num3, num4);
				color4 = Main.buffColor(color4, num, num2, num3, num4);
				color5 = Main.buffColor(color5, num, num2, num3, num4);
				color6 = Main.buffColor(color6, num, num2, num3, num4);
				color7 = Main.buffColor(color7, num, num2, num3, num4);
				color8 = Main.buffColor(color8, num, num2, num3, num4);
				color9 = Main.buffColor(color9, num, num2, num3, num4);
				color10 = Main.buffColor(color10, num, num2, num3, num4);
				color11 = Main.buffColor(color11, num, num2, num3, num4);
				color12 = Main.buffColor(color12, num, num2, num3, num4);
			}
			if (drawPlayer.gravDir == 1f)
			{
				if (drawPlayer.direction == 1)
				{
					effects = SpriteEffects.None;
					effects2 = SpriteEffects.None;
				}
				else
				{
					effects = SpriteEffects.FlipHorizontally;
					effects2 = SpriteEffects.FlipHorizontally;
				}
				if (!drawPlayer.dead)
				{
					drawPlayer.legPosition.Y = 0f;
					drawPlayer.headPosition.Y = 0f;
					drawPlayer.bodyPosition.Y = 0f;
				}
			}
			else
			{
				if (drawPlayer.direction == 1)
				{
					effects = SpriteEffects.FlipVertically;
					effects2 = SpriteEffects.FlipVertically;
				}
				else
				{
					effects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
					effects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
				}
				if (!drawPlayer.dead)
				{
					drawPlayer.legPosition.Y = 6f;
					drawPlayer.headPosition.Y = 6f;
					drawPlayer.bodyPosition.Y = 6f;
				}
			}
			Vector2 vector = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.75f);
			Vector2 origin = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.5f);
			Vector2 vector2 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.25f);
			if (drawPlayer.legs > 0 && drawPlayer.legs < 16)
			{
				this.spriteBatch.Draw(Main.armorLegTexture[drawPlayer.legs], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color12, drawPlayer.legRotation, vector, 1f, effects, 0f);
			}
			else
			{
				if (!drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerPantsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color8, drawPlayer.legRotation, vector, 1f, effects, 0f);
					this.spriteBatch.Draw(Main.playerShoesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f))) + drawPlayer.legPosition + vector, new Rectangle?(drawPlayer.legFrame), color9, drawPlayer.legRotation, vector, 1f, effects, 0f);
				}
			}
			if (drawPlayer.body > 0 && drawPlayer.body < 17)
			{
				this.spriteBatch.Draw(Main.armorBodyTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				if ((drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16) && !drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
			}
			else
			{
				if (!drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerUnderShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					this.spriteBatch.Draw(Main.playerShirtTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color6, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
			}
			if (!drawPlayer.invis)
			{
				this.spriteBatch.Draw(Main.playerHeadTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color4, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				this.spriteBatch.Draw(Main.playerEyeWhitesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				this.spriteBatch.Draw(Main.playerEyesTexture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color2, drawPlayer.headRotation, vector2, 1f, effects, 0f);
			}
			if (drawPlayer.head == 10 || drawPlayer.head == 12 || drawPlayer.head == 28)
			{
				this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				if (!drawPlayer.invis)
				{
					Rectangle bodyFrame = drawPlayer.bodyFrame;
					bodyFrame.Y -= 336;
					if (bodyFrame.Y < 0)
					{
						bodyFrame.Y = 0;
					}
					this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
			}
			if (drawPlayer.head == 23)
			{
				Rectangle bodyFrame2 = drawPlayer.bodyFrame;
				bodyFrame2.Y -= 336;
				if (bodyFrame2.Y < 0)
				{
					bodyFrame2.Y = 0;
				}
				if (!drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame2), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
				this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
			}
			else
			{
				if (drawPlayer.head == 14)
				{
					Rectangle bodyFrame3 = drawPlayer.bodyFrame;
					int num7 = 0;
					if (bodyFrame3.Y == bodyFrame3.Height * 6)
					{
						bodyFrame3.Height -= 2;
					}
					else
					{
						if (bodyFrame3.Y == bodyFrame3.Height * 7)
						{
							num7 = -2;
						}
						else
						{
							if (bodyFrame3.Y == bodyFrame3.Height * 8)
							{
								num7 = -2;
							}
							else
							{
								if (bodyFrame3.Y == bodyFrame3.Height * 9)
								{
									num7 = -2;
								}
								else
								{
									if (bodyFrame3.Y == bodyFrame3.Height * 10)
									{
										num7 = -2;
									}
									else
									{
										if (bodyFrame3.Y == bodyFrame3.Height * 13)
										{
											bodyFrame3.Height -= 2;
										}
										else
										{
											if (bodyFrame3.Y == bodyFrame3.Height * 14)
											{
												num7 = -2;
											}
											else
											{
												if (bodyFrame3.Y == bodyFrame3.Height * 15)
												{
													num7 = -2;
												}
												else
												{
													if (bodyFrame3.Y == bodyFrame3.Height * 16)
													{
														num7 = -2;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					bodyFrame3.Y += num7;
					this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f + (float)num7))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame3), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
				}
				else
				{
					if (drawPlayer.head > 0 && drawPlayer.head < 29 && drawPlayer.head != 28)
					{
						this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(drawPlayer.bodyFrame), color10, drawPlayer.headRotation, vector2, 1f, effects, 0f);
					}
					else
					{
						if (!drawPlayer.invis)
						{
							Rectangle bodyFrame4 = drawPlayer.bodyFrame;
							bodyFrame4.Y -= 336;
							if (bodyFrame4.Y < 0)
							{
								bodyFrame4.Y = 0;
							}
							this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.headPosition + vector2, new Rectangle?(bodyFrame4), color3, drawPlayer.headRotation, vector2, 1f, effects, 0f);
						}
					}
				}
			}
			if (drawPlayer.heldProj >= 0)
			{
				this.DrawProj(drawPlayer.heldProj);
			}
			Color color13 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
			if ((drawPlayer.itemAnimation > 0 || drawPlayer.inventory[drawPlayer.selectedItem].holdStyle > 0) && drawPlayer.inventory[drawPlayer.selectedItem].type > 0 && !drawPlayer.dead && !drawPlayer.inventory[drawPlayer.selectedItem].noUseGraphic && (!drawPlayer.wet || !drawPlayer.inventory[drawPlayer.selectedItem].noWet))
			{
				if (drawPlayer.inventory[drawPlayer.selectedItem].useStyle == 5)
				{
					int num8 = 10;
					Vector2 vector3 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width / 2), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
					if (drawPlayer.inventory[drawPlayer.selectedItem].type == 95)
					{
						num8 = 10;
						vector3.Y += 2f * drawPlayer.gravDir;
					}
					else
					{
						if (drawPlayer.inventory[drawPlayer.selectedItem].type == 96)
						{
							num8 = -5;
						}
						else
						{
							if (drawPlayer.inventory[drawPlayer.selectedItem].type == 98)
							{
								num8 = -5;
								vector3.Y -= 2f * drawPlayer.gravDir;
							}
							else
							{
								if (drawPlayer.inventory[drawPlayer.selectedItem].type == 197)
								{
									num8 = -5;
									vector3.Y += 4f * drawPlayer.gravDir;
								}
								else
								{
									if (drawPlayer.inventory[drawPlayer.selectedItem].type == 126)
									{
										num8 = 4;
										vector3.Y += 4f * drawPlayer.gravDir;
									}
									else
									{
										if (drawPlayer.inventory[drawPlayer.selectedItem].type == 127)
										{
											num8 = 4;
											vector3.Y += 2f * drawPlayer.gravDir;
										}
										else
										{
											if (drawPlayer.inventory[drawPlayer.selectedItem].type == 157)
											{
												num8 = 6;
												vector3.Y += 2f * drawPlayer.gravDir;
											}
											else
											{
												if (drawPlayer.inventory[drawPlayer.selectedItem].type == 160)
												{
													num8 = -8;
												}
												else
												{
													if (drawPlayer.inventory[drawPlayer.selectedItem].type == 164 || drawPlayer.inventory[drawPlayer.selectedItem].type == 219)
													{
														num8 = 2;
														vector3.Y += 4f * drawPlayer.gravDir;
													}
													else
													{
														if (drawPlayer.inventory[drawPlayer.selectedItem].type == 165 || drawPlayer.inventory[drawPlayer.selectedItem].type == 272)
														{
															num8 = 4;
															vector3.Y += 4f * drawPlayer.gravDir;
														}
														else
														{
															if (drawPlayer.inventory[drawPlayer.selectedItem].type == 266)
															{
																num8 = 0;
																vector3.Y += 2f * drawPlayer.gravDir;
															}
															else
															{
																if (drawPlayer.inventory[drawPlayer.selectedItem].type == 281)
																{
																	num8 = 6;
																	vector3.Y -= 6f * drawPlayer.gravDir;
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					Vector2 origin2 = new Vector2((float)(-(float)num8), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
					if (drawPlayer.direction == -1)
					{
						origin2 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width + num8), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
					}
					this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X + vector3.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y + vector3.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
					if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
					{
						this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X + vector3.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y + vector3.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
					}
				}
				else
				{
					if (drawPlayer.gravDir == -1f)
					{
						this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						}
					}
					else
					{
						this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)((int)(drawPlayer.itemLocation.X - Main.screenPosition.X)), (float)((int)(drawPlayer.itemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0f);
						}
					}
				}
			}
			if (drawPlayer.body > 0 && drawPlayer.body < 17)
			{
				this.spriteBatch.Draw(Main.armorArmTexture[drawPlayer.body], new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color11, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				if ((drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16) && !drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					return;
				}
			}
			else
			{
				if (!drawPlayer.invis)
				{
					this.spriteBatch.Draw(Main.playerUnderShirt2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color7, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
					this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)((int)(drawPlayer.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawPlayer.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f))) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), color5, drawPlayer.bodyRotation, origin, 1f, effects, 0f);
				}
			}
		}
		private static void HelpText()
		{
			bool flag = false;
			if (Main.player[Main.myPlayer].statLifeMax > 100)
			{
				flag = true;
			}
			bool flag2 = false;
			if (Main.player[Main.myPlayer].statManaMax > 0)
			{
				flag2 = true;
			}
			bool flag3 = true;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			for (int i = 0; i < 44; i++)
			{
				if (Main.player[Main.myPlayer].inventory[i].pick > 0 && Main.player[Main.myPlayer].inventory[i].name != "Copper Pickaxe")
				{
					flag3 = false;
				}
				if (Main.player[Main.myPlayer].inventory[i].axe > 0 && Main.player[Main.myPlayer].inventory[i].name != "Copper Axe")
				{
					flag3 = false;
				}
				if (Main.player[Main.myPlayer].inventory[i].hammer > 0)
				{
					flag3 = false;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 11 || Main.player[Main.myPlayer].inventory[i].type == 12 || Main.player[Main.myPlayer].inventory[i].type == 13 || Main.player[Main.myPlayer].inventory[i].type == 14)
				{
					flag4 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 19 || Main.player[Main.myPlayer].inventory[i].type == 20 || Main.player[Main.myPlayer].inventory[i].type == 21 || Main.player[Main.myPlayer].inventory[i].type == 22)
				{
					flag5 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 75)
				{
					flag6 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 75)
				{
					flag8 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 68 || Main.player[Main.myPlayer].inventory[i].type == 70)
				{
					flag9 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 84)
				{
					flag10 = true;
				}
				if (Main.player[Main.myPlayer].inventory[i].type == 117)
				{
					flag7 = true;
				}
			}
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			for (int j = 0; j < 1000; j++)
			{
				if (Main.npc[j].active)
				{
					if (Main.npc[j].type == 17)
					{
						flag11 = true;
					}
					if (Main.npc[j].type == 18)
					{
						flag12 = true;
					}
					if (Main.npc[j].type == 19)
					{
						flag14 = true;
					}
					if (Main.npc[j].type == 20)
					{
						flag13 = true;
					}
				}
			}
			while (true)
			{
				Main.helpText++;
				if (flag3)
				{
					if (Main.helpText == 1)
					{
						break;
					}
					if (Main.helpText == 2)
					{
						goto Block_27;
					}
					if (Main.helpText == 3)
					{
						goto Block_28;
					}
					if (Main.helpText == 4)
					{
						goto Block_29;
					}
					if (Main.helpText == 5)
					{
						goto Block_30;
					}
					if (Main.helpText == 6)
					{
						goto Block_31;
					}
				}
				if (flag3 && !flag4 && !flag5 && Main.helpText == 11)
				{
					goto Block_35;
				}
				if (flag3 && flag4 && !flag5)
				{
					if (Main.helpText == 21)
					{
						goto Block_39;
					}
					if (Main.helpText == 22)
					{
						goto Block_40;
					}
				}
				if (flag3 && flag5)
				{
					if (Main.helpText == 31)
					{
						goto Block_43;
					}
					if (Main.helpText == 32)
					{
						goto Block_44;
					}
				}
				if (!flag && Main.helpText == 41)
				{
					goto Block_46;
				}
				if (!flag2 && Main.helpText == 42)
				{
					goto Block_48;
				}
				if (!flag2 && !flag6 && Main.helpText == 43)
				{
					goto Block_51;
				}
				if (!flag11 && !flag12)
				{
					if (Main.helpText == 51)
					{
						goto Block_54;
					}
					if (Main.helpText == 52)
					{
						goto Block_55;
					}
					if (Main.helpText == 53)
					{
						goto Block_56;
					}
				}
				if (!flag11 && Main.helpText == 61)
				{
					goto Block_58;
				}
				if (!flag12 && Main.helpText == 62)
				{
					goto Block_60;
				}
				if (!flag14 && Main.helpText == 63)
				{
					goto Block_62;
				}
				if (!flag13 && Main.helpText == 64)
				{
					goto Block_64;
				}
				if (flag8 && Main.helpText == 71)
				{
					goto Block_66;
				}
				if (flag9 && Main.helpText == 72)
				{
					goto Block_68;
				}
				if ((flag8 || flag9) && Main.helpText == 80)
				{
					goto Block_70;
				}
				if (!flag10 && Main.helpText == 201)
				{
					goto Block_72;
				}
				if (flag7 && Main.helpText == 202)
				{
					goto Block_74;
				}
				if (Main.helpText == 1000)
				{
					goto Block_75;
				}
				if (Main.helpText == 1001)
				{
					goto Block_76;
				}
				if (Main.helpText == 1002)
				{
					goto Block_77;
				}
				if (Main.helpText > 1100)
				{
					Main.helpText = 0;
				}
			}
			Main.npcChatText = "You can use your pickaxe to dig through dirt, and your axe to chop down trees. Just place your cursor over the tile and click!";
			return;
			Block_27:
			Main.npcChatText = "If you want to survive, you will need to create weapons and shelter. Start by chopping down trees and gathering wood.";
			return;
			Block_28:
			Main.npcChatText = "Press ESC to access your crafting menu. When you have enough wood, create a workbench. This will allow you to create more complicated things, as long as you are standing close to it.";
			return;
			Block_29:
			Main.npcChatText = "You can build a shelter by placing wood or other blocks in the world. Don't forget to create and place walls.";
			return;
			Block_30:
			Main.npcChatText = "Once you have a wooden sword, you might try to gather some gel from the slimes. Combine wood and gel to make a torch!";
			return;
			Block_31:
			Main.npcChatText = "To interact with backgrounds and placed objects, use a hammer!";
			return;
			Block_35:
			Main.npcChatText = "You should do some mining to find metal ore. You can craft very useful things with it.";
			return;
			Block_39:
			Main.npcChatText = "Now that you have some ore, you will need to turn it into a bar in order to make items with it. This requires a furnace!";
			return;
			Block_40:
			Main.npcChatText = "You can create a furnace out of torches, wood, and stone. Make sure you are standing near a work bench.";
			return;
			Block_43:
			Main.npcChatText = "You will need an anvil to make most things out of metal bars.";
			return;
			Block_44:
			Main.npcChatText = "Anvils can be crafted out of iron, or purchased from a merchant.";
			return;
			Block_46:
			Main.npcChatText = "Underground are crystal hearts that can be used to increase your max life. You will need a hammer to obtain them.";
			return;
			Block_48:
			Main.npcChatText = "If you gather 10 fallen stars, they can be combined to create an item that will increase your magic capacity.";
			return;
			Block_51:
			Main.npcChatText = "Stars fall all over the world at night. They can be used for all sorts of usefull things. If you see one, be sure to grab it because they disappear after sunrise.";
			return;
			Block_54:
			Main.npcChatText = "There are many different ways you can attract people to move in to our town. They will of course need a home to live in.";
			return;
			Block_55:
			Main.npcChatText = "In order for a room to be considered a home, it needs to have a door, chair, table, and a light source.  Make sure the house has walls as well.";
			return;
			Block_56:
			Main.npcChatText = "Two people will not live in the same home. Also, if their home is destroyed, they will look for a new place to live.";
			return;
			Block_58:
			Main.npcChatText = "If you want a merchant to move in, you will need to gather plenty of money. 50 silver coins should do the trick!";
			return;
			Block_60:
			Main.npcChatText = "For a nurse to move in, you might want to increase your maximum life.";
			return;
			Block_62:
			Main.npcChatText = "If you had a gun, I bet an arms dealer might show up to sell you some ammo!";
			return;
			Block_64:
			Main.npcChatText = "You should prove yourself by defeating a strong monster. That will get the attention of a dryad.";
			return;
			Block_66:
			Main.npcChatText = "If you combine lenses at a demon altar, you might be able to find a way to summon a powerful monster. You will want to wait until night before using it, though.";
			return;
			Block_68:
			Main.npcChatText = "You can create worm bait with rotten chunks and vile powder. Make sure you are in a corrupt area before using it.";
			return;
			Block_70:
			Main.npcChatText = "Demonic altars can usually be found in the corruption. You will need to be near them to craft some items.";
			return;
			Block_72:
			Main.npcChatText = "You can make a grappling hook from a hook and 3 chains. Skeletons found deep underground usually carry hooks, and chains can be made from iron bars.";
			return;
			Block_74:
			Main.npcChatText = "You can craft a space gun using a flintlock pistol, 10 fallen stars, and 30 meteorite bars.";
			return;
			Block_75:
			Main.npcChatText = "If you see a pot, be sure to smash it open. They contain all sorts of useful supplies.";
			return;
			Block_76:
			Main.npcChatText = "There is treasure hidden all over the world. Some amazing things can be found deep underground!";
			return;
			Block_77:
			Main.npcChatText = "Smashing a shadow orb will cause a meteor to fall out of the sky. Shadow orbs can usually be found in the chasms around corrupt areas.";
		}
		protected void DrawChat()
		{
			if (Main.player[Main.myPlayer].talkNPC < 0 && Main.player[Main.myPlayer].sign == -1)
			{
				Main.npcChatText = "";
				return;
			}
			Color color = new Color(200, 200, 200, 200);
			int num = (int)((Main.mouseTextColor * 2 + 255) / 3);
			Color color2 = new Color(num, num, num, num);
			int num2 = 10;
			int num3 = 0;
			string[] array = new string[num2];
			int num4 = 0;
			int num5 = 0;
			if (Main.npcChatText == null)
			{
				Main.npcChatText = "";
			}
			for (int i = 0; i < Main.npcChatText.Length; i++)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(Main.npcChatText.Substring(i, 1));
				if (bytes[0] == 10)
				{
					array[num3] = Main.npcChatText.Substring(num4, i - num4);
					num3++;
					num4 = i + 1;
					num5 = i + 1;
				}
				else
				{
					if (Main.npcChatText.Substring(i, 1) == " " || i == Main.npcChatText.Length - 1)
					{
						if (Main.fontMouseText.MeasureString(Main.npcChatText.Substring(num4, i - num4)).X > 470f)
						{
							array[num3] = Main.npcChatText.Substring(num4, num5 - num4);
							num3++;
							num4 = num5 + 1;
						}
						num5 = i;
					}
				}
				if (num3 == 10)
				{
					Main.npcChatText = Main.npcChatText.Substring(0, i - 1);
					num4 = i - 1;
					num3 = 9;
					break;
				}
			}
			if (num3 < 10)
			{
				array[num3] = Main.npcChatText.Substring(num4, Main.npcChatText.Length - num4);
			}
			if (Main.editSign)
			{
				this.textBlinkerCount++;
				if (this.textBlinkerCount >= 20)
				{
					if (this.textBlinkerState == 0)
					{
						this.textBlinkerState = 1;
					}
					else
					{
						this.textBlinkerState = 0;
					}
					this.textBlinkerCount = 0;
				}
				if (this.textBlinkerState == 1)
				{
					string[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)num3)] = array2[(int)intPtr] + "|";
				}
			}
			num3++;
			this.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.chatBackTexture.Width, (num3 + 1) * 30)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			this.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), (float)(100 + (num3 + 1) * 30)), new Rectangle?(new Rectangle(0, Main.chatBackTexture.Height - 30, Main.chatBackTexture.Width, 30)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			for (int j = 0; j < num3; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					Color color3 = Color.Black;
					int num6 = 170 + (Main.screenWidth - 800) / 2;
					int num7 = 120 + j * 30;
					if (k == 0)
					{
						num6 -= 2;
					}
					if (k == 1)
					{
						num6 += 2;
					}
					if (k == 2)
					{
						num7 -= 2;
					}
					if (k == 3)
					{
						num7 += 2;
					}
					if (k == 4)
					{
						color3 = color2;
					}
					this.spriteBatch.DrawString(Main.fontMouseText, array[j], new Vector2((float)num6, (float)num7), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			num = (int)Main.mouseTextColor;
			color2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
			string text = "";
			string text2 = "";
			int num8 = Main.player[Main.myPlayer].statLifeMax - Main.player[Main.myPlayer].statLife;
			if (Main.player[Main.myPlayer].sign > -1)
			{
				if (Main.editSign)
				{
					text = "Save";
				}
				else
				{
					text = "Edit";
				}
			}
			else
			{
				if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 17 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 19 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 38 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 54)
				{
					text = "Shop";
				}
				else
				{
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 37)
					{
						if (!Main.dayTime)
						{
							text = "Curse";
						}
					}
					else
					{
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
						{
							text = "Help";
							text2 = "Crafting";
						}
						else
						{
							if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 18)
							{
								string text3 = "";
								int num9 = 0;
								int num10 = 0;
								int num11 = 0;
								int num12 = 0;
								int num13 = num8;
								if (num13 > 0)
								{
									num13 = (int)((double)num13 * 0.75);
									if (num13 < 1)
									{
										num13 = 1;
									}
								}
								if (num13 < 0)
								{
									num13 = 0;
								}
								num8 = num13;
								if (num13 >= 1000000)
								{
									num9 = num13 / 1000000;
									num13 -= num9 * 1000000;
								}
								if (num13 >= 10000)
								{
									num10 = num13 / 10000;
									num13 -= num10 * 10000;
								}
								if (num13 >= 100)
								{
									num11 = num13 / 100;
									num13 -= num11 * 100;
								}
								if (num13 >= 1)
								{
									num12 = num13;
								}
								if (num9 > 0)
								{
									text3 = text3 + num9 + " platinum ";
								}
								if (num10 > 0)
								{
									text3 = text3 + num10 + " gold ";
								}
								if (num11 > 0)
								{
									text3 = text3 + num11 + " silver ";
								}
								if (num12 > 0)
								{
									text3 = text3 + num12 + " copper ";
								}
								float num14 = (float)Main.mouseTextColor / 255f;
								if (num9 > 0)
								{
									color2 = new Color((int)((byte)(220f * num14)), (int)((byte)(220f * num14)), (int)((byte)(198f * num14)), (int)Main.mouseTextColor);
								}
								else
								{
									if (num10 > 0)
									{
										color2 = new Color((int)((byte)(224f * num14)), (int)((byte)(201f * num14)), (int)((byte)(92f * num14)), (int)Main.mouseTextColor);
									}
									else
									{
										if (num11 > 0)
										{
											color2 = new Color((int)((byte)(181f * num14)), (int)((byte)(192f * num14)), (int)((byte)(193f * num14)), (int)Main.mouseTextColor);
										}
										else
										{
											if (num12 > 0)
											{
												color2 = new Color((int)((byte)(246f * num14)), (int)((byte)(138f * num14)), (int)((byte)(96f * num14)), (int)Main.mouseTextColor);
											}
										}
									}
								}
								text = "Heal (" + text3 + ")";
								if (num13 == 0)
								{
									text = "Heal";
								}
							}
						}
					}
				}
			}
			int num15 = 180 + (Main.screenWidth - 800) / 2;
			int num16 = 130 + num3 * 30;
			float scale = 0.9f;
			if (Main.mouseState.X > num15 && (float)Main.mouseState.X < (float)num15 + Main.fontMouseText.MeasureString(text).X && Main.mouseState.Y > num16 && (float)Main.mouseState.Y < (float)num16 + Main.fontMouseText.MeasureString(text).Y)
			{
				Main.player[Main.myPlayer].mouseInterface = true;
				scale = 1.1f;
				if (!Main.npcChatFocus2)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus2 = true;
				Main.player[Main.myPlayer].releaseUseItem = false;
			}
			else
			{
				if (Main.npcChatFocus2)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus2 = false;
			}
			for (int l = 0; l < 5; l++)
			{
				int num17 = num15;
				int num18 = num16;
				Color color4 = Color.Black;
				if (l == 0)
				{
					num17 -= 2;
				}
				if (l == 1)
				{
					num17 += 2;
				}
				if (l == 2)
				{
					num18 -= 2;
				}
				if (l == 3)
				{
					num18 += 2;
				}
				if (l == 4)
				{
					color4 = color2;
				}
				Vector2 vector = Main.fontMouseText.MeasureString(text);
				vector *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)num17 + vector.X, (float)num18 + vector.Y), color4, 0f, vector, scale, SpriteEffects.None, 0f);
			}
			color2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
			num15 = num15 + (int)Main.fontMouseText.MeasureString(text).X + 20;
			num16 = 130 + num3 * 30;
			scale = 0.9f;
			if (Main.mouseState.X > num15 && (float)Main.mouseState.X < (float)num15 + Main.fontMouseText.MeasureString("Close").X && Main.mouseState.Y > num16 && (float)Main.mouseState.Y < (float)num16 + Main.fontMouseText.MeasureString("Close").Y)
			{
				scale = 1.1f;
				if (!Main.npcChatFocus1)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus1 = true;
				Main.player[Main.myPlayer].releaseUseItem = false;
				Main.player[Main.myPlayer].controlUseItem = false;
			}
			else
			{
				if (Main.npcChatFocus1)
				{
					Main.PlaySound(12, -1, -1, 1);
				}
				Main.npcChatFocus1 = false;
			}
			for (int m = 0; m < 5; m++)
			{
				int num19 = num15;
				int num20 = num16;
				Color color5 = Color.Black;
				if (m == 0)
				{
					num19 -= 2;
				}
				if (m == 1)
				{
					num19 += 2;
				}
				if (m == 2)
				{
					num20 -= 2;
				}
				if (m == 3)
				{
					num20 += 2;
				}
				if (m == 4)
				{
					color5 = color2;
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString("Close");
				vector2 *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, "Close", new Vector2((float)num19 + vector2.X, (float)num20 + vector2.Y), color5, 0f, vector2, scale, SpriteEffects.None, 0f);
			}
			if (text2 != "")
			{
				num15 = 296 + (Main.screenWidth - 800) / 2;
				num16 = 130 + num3 * 30;
				scale = 0.9f;
				if (Main.mouseState.X > num15 && (float)Main.mouseState.X < (float)num15 + Main.fontMouseText.MeasureString(text2).X && Main.mouseState.Y > num16 && (float)Main.mouseState.Y < (float)num16 + Main.fontMouseText.MeasureString(text2).Y)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					scale = 1.1f;
					if (!Main.npcChatFocus3)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					Main.npcChatFocus3 = true;
					Main.player[Main.myPlayer].releaseUseItem = false;
				}
				else
				{
					if (Main.npcChatFocus3)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					Main.npcChatFocus3 = false;
				}
				for (int n = 0; n < 5; n++)
				{
					int num21 = num15;
					int num22 = num16;
					Color color6 = Color.Black;
					if (n == 0)
					{
						num21 -= 2;
					}
					if (n == 1)
					{
						num21 += 2;
					}
					if (n == 2)
					{
						num22 -= 2;
					}
					if (n == 3)
					{
						num22 += 2;
					}
					if (n == 4)
					{
						color6 = color2;
					}
					Vector2 vector3 = Main.fontMouseText.MeasureString(text);
					vector3 *= 0.5f;
					this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)num21 + vector3.X, (float)num22 + vector3.Y), color6, 0f, vector3, scale, SpriteEffects.None, 0f);
				}
			}
			if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease)
			{
				Main.mouseLeftRelease = false;
				Main.player[Main.myPlayer].releaseUseItem = false;
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.npcChatFocus1)
				{
					Main.player[Main.myPlayer].talkNPC = -1;
					Main.player[Main.myPlayer].sign = -1;
					Main.editSign = false;
					Main.npcChatText = "";
					Main.PlaySound(11, -1, -1, 1);
					return;
				}
				if (Main.npcChatFocus2)
				{
					if (Main.player[Main.myPlayer].sign != -1)
					{
						if (!Main.editSign)
						{
							Main.PlaySound(12, -1, -1, 1);
							Main.editSign = true;
							return;
						}
						Main.PlaySound(12, -1, -1, 1);
						int num23 = Main.player[Main.myPlayer].sign;
						Sign.TextSign(num23, Main.npcChatText);
						Main.editSign = false;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(47, -1, -1, "", num23, 0f, 0f, 0f, 0);
							return;
						}
					}
					else
					{
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 17)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 1;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 19)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 2;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 37)
						{
							if (Main.netMode == 0)
							{
								NPC.SpawnSkeletron();
							}
							else
							{
								NetMessage.SendData(51, -1, -1, "", Main.myPlayer, 1f, 0f, 0f, 0);
							}
							Main.npcChatText = "";
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 3;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 38)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 4;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 54)
						{
							Main.playerInventory = true;
							Main.npcChatText = "";
							Main.npcShop = 5;
							this.shop[Main.npcShop].SetupShop(Main.npcShop);
							Main.PlaySound(12, -1, -1, 1);
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
						{
							Main.PlaySound(12, -1, -1, 1);
							Main.HelpText();
							return;
						}
						if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 18)
						{
							Main.PlaySound(12, -1, -1, 1);
							if (num8 > 0)
							{
								if (Main.player[Main.myPlayer].BuyItem(num8))
								{
									Main.PlaySound(2, -1, -1, 4);
									Main.player[Main.myPlayer].HealEffect(Main.player[Main.myPlayer].statLifeMax - Main.player[Main.myPlayer].statLife);
									if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.25)
									{
										Main.npcChatText = "I managed to sew your face back on. Be more careful next time.";
									}
									else
									{
										if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.5)
										{
											Main.npcChatText = "That's probably going to leave a scar.";
										}
										else
										{
											if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.75)
											{
												Main.npcChatText = "All better. I don't want to see you jumping off anymore cliffs.";
											}
											else
											{
												Main.npcChatText = "That didn't hurt too bad, now did it?";
											}
										}
									}
									Main.player[Main.myPlayer].statLife = Main.player[Main.myPlayer].statLifeMax;
									return;
								}
								int num24 = Main.rand.Next(3);
								if (num24 == 0)
								{
									Main.npcChatText = "I'm sorry, but you can't afford me.";
								}
								if (num24 == 1)
								{
									Main.npcChatText = "I'm gonna need more gold than that.";
								}
								if (num24 == 2)
								{
									Main.npcChatText = "I don't work for free you know.";
									return;
								}
							}
							else
							{
								int num25 = Main.rand.Next(3);
								if (num25 == 0)
								{
									Main.npcChatText = "I don't give happy endings.";
								}
								if (num25 == 1)
								{
									Main.npcChatText = "I can't do anymore for you without plastic surgery.";
								}
								if (num25 == 2)
								{
									Main.npcChatText = "Quit wasting my time.";
									return;
								}
							}
						}
					}
				}
				else
				{
					if (Main.npcChatFocus3 && Main.player[Main.myPlayer].talkNPC >= 0 && Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
					{
						Main.playerInventory = true;
						Main.npcChatText = "";
						Main.PlaySound(12, -1, -1, 1);
						Main.craftGuide = true;
					}
				}
			}
		}
		private static bool AccCheck(Item newItem)
		{
			for (int i = 0; i < Main.player[Main.myPlayer].armor.Length; i++)
			{
				if (newItem.IsTheSameAs(Main.player[Main.myPlayer].armor[i]))
				{
					return true;
				}
			}
			return false;
		}
		public static Item armorSwap(Item newItem)
		{
			if (newItem.headSlot == -1 && newItem.bodySlot == -1 && newItem.legSlot == -1 && !newItem.accessory)
			{
				return newItem;
			}
			Item result = newItem;
			if (newItem.headSlot != -1)
			{
				result = (Item)Main.player[Main.myPlayer].armor[0].Clone();
				Main.player[Main.myPlayer].armor[0] = (Item)newItem.Clone();
			}
			else
			{
				if (newItem.bodySlot != -1)
				{
					result = (Item)Main.player[Main.myPlayer].armor[1].Clone();
					Main.player[Main.myPlayer].armor[1] = (Item)newItem.Clone();
				}
				else
				{
					if (newItem.legSlot != -1)
					{
						result = (Item)Main.player[Main.myPlayer].armor[2].Clone();
						Main.player[Main.myPlayer].armor[2] = (Item)newItem.Clone();
					}
					else
					{
						if (newItem.accessory)
						{
							if (Main.AccCheck(newItem))
							{
								return result;
							}
							for (int i = 3; i < 8; i++)
							{
								if (Main.player[Main.myPlayer].armor[i].type == 0)
								{
									Main.accSlotCount = i - 3;
									break;
								}
							}
							result = (Item)Main.player[Main.myPlayer].armor[3 + Main.accSlotCount].Clone();
							Main.player[Main.myPlayer].armor[3 + Main.accSlotCount] = (Item)newItem.Clone();
							Main.accSlotCount++;
							if (Main.accSlotCount >= 5)
							{
								Main.accSlotCount = 0;
							}
						}
					}
				}
			}
			Main.PlaySound(7, -1, -1, 1);
			Recipe.FindRecipes();
			return result;
		}
		public static void BankCoins()
		{
			for (int i = 0; i < 20; i++)
			{
				if (Main.player[Main.myPlayer].bank[i].type >= 71 && Main.player[Main.myPlayer].bank[i].type <= 73 && Main.player[Main.myPlayer].bank[i].stack == Main.player[Main.myPlayer].bank[i].maxStack)
				{
					Main.player[Main.myPlayer].bank[i].SetDefaults(Main.player[Main.myPlayer].bank[i].type + 1, false);
					for (int j = 0; j < 20; j++)
					{
						if (j != i && Main.player[Main.myPlayer].bank[j].type == Main.player[Main.myPlayer].bank[i].type && Main.player[Main.myPlayer].bank[j].stack < Main.player[Main.myPlayer].bank[j].maxStack)
						{
							Main.player[Main.myPlayer].bank[j].stack++;
							Main.player[Main.myPlayer].bank[i].SetDefaults(0, false);
							Main.BankCoins();
						}
					}
				}
			}
		}
		public static void ChestCoins()
		{
			for (int i = 0; i < 20; i++)
			{
				if (Main.chest[Main.player[Main.myPlayer].chest].item[i].type >= 71 && Main.chest[Main.player[Main.myPlayer].chest].item[i].type <= 73 && Main.chest[Main.player[Main.myPlayer].chest].item[i].stack == Main.chest[Main.player[Main.myPlayer].chest].item[i].maxStack)
				{
					Main.chest[Main.player[Main.myPlayer].chest].item[i].SetDefaults(Main.chest[Main.player[Main.myPlayer].chest].item[i].type + 1, false);
					for (int j = 0; j < 20; j++)
					{
						if (j != i && Main.chest[Main.player[Main.myPlayer].chest].item[j].type == Main.chest[Main.player[Main.myPlayer].chest].item[i].type && Main.chest[Main.player[Main.myPlayer].chest].item[j].stack < Main.chest[Main.player[Main.myPlayer].chest].item[j].maxStack)
						{
							Main.chest[Main.player[Main.myPlayer].chest].item[j].stack++;
							Main.chest[Main.player[Main.myPlayer].chest].item[i].SetDefaults(0, false);
							Main.ChestCoins();
						}
					}
				}
			}
		}
		protected void DrawInterface()
		{
			Main.mouseHC = false;
			if (Main.hideUI)
			{
				return;
			}
			Vector2 origin;
			if (Main.signBubble)
			{
				int num = (int)((float)Main.signX - Main.screenPosition.X);
				int num2 = (int)((float)Main.signY - Main.screenPosition.Y);
				SpriteEffects effects = SpriteEffects.None;
				if ((float)Main.signX > Main.player[Main.myPlayer].position.X + (float)Main.player[Main.myPlayer].width)
				{
					effects = SpriteEffects.FlipHorizontally;
					num += -8 - Main.chat2Texture.Width;
				}
				else
				{
					num += 8;
				}
				num2 -= 22;
				SpriteBatch arg_F7_0 = this.spriteBatch;
				Texture2D arg_F7_1 = Main.chat2Texture;
				Vector2 arg_F7_2 = new Vector2((float)num, (float)num2);
				Rectangle? arg_F7_3 = new Rectangle?(new Rectangle(0, 0, Main.chat2Texture.Width, Main.chat2Texture.Height));
				Color arg_F7_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_F7_5 = 0f;
				origin = default(Vector2);
				arg_F7_0.Draw(arg_F7_1, arg_F7_2, arg_F7_3, arg_F7_4, arg_F7_5, origin, 1f, effects, 0f);
				Main.signBubble = false;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active && Main.myPlayer != i && !Main.player[i].dead)
				{
					new Rectangle((int)((double)Main.player[i].position.X + (double)Main.player[i].width * 0.5 - 16.0), (int)(Main.player[i].position.Y + (float)Main.player[i].height - 48f), 32, 48);
					if (Main.player[Main.myPlayer].team > 0 && Main.player[Main.myPlayer].team == Main.player[i].team)
					{
						new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
						string text = Main.player[i].name;
						if (Main.player[i].statLife < Main.player[i].statLifeMax)
						{
							object obj = text;
							text = string.Concat(new object[]
							{
								obj, 
								": ", 
								Main.player[i].statLife, 
								"/", 
								Main.player[i].statLifeMax
							});
						}
						Vector2 vector = Main.fontMouseText.MeasureString(text);
						float num3 = 0f;
						if (Main.player[i].chatShowTime > 0)
						{
							num3 = -vector.Y;
						}
						float num4 = 0f;
						float num5 = (float)Main.mouseTextColor / 255f;
						Color color = new Color((int)((byte)((float)Main.teamColor[Main.player[i].team].R * num5)), (int)((byte)((float)Main.teamColor[Main.player[i].team].G * num5)), (int)((byte)((float)Main.teamColor[Main.player[i].team].B * num5)), (int)Main.mouseTextColor);
						Vector2 vector2 = new Vector2((float)(Main.screenWidth / 2) + Main.screenPosition.X, (float)(Main.screenHeight / 2) + Main.screenPosition.Y);
						float num6 = Main.player[i].position.X + (float)(Main.player[i].width / 2) - vector2.X;
						float num7 = Main.player[i].position.Y - vector.Y - 2f + num3 - vector2.Y;
						float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
						int num9 = Main.screenHeight;
						if (Main.screenHeight > Main.screenWidth)
						{
							num9 = Main.screenWidth;
						}
						num9 = num9 / 2 - 30;
						if (num9 < 100)
						{
							num9 = 100;
						}
						if (num8 < (float)num9)
						{
							vector.X = Main.player[i].position.X + (float)(Main.player[i].width / 2) - vector.X / 2f - Main.screenPosition.X;
							vector.Y = Main.player[i].position.Y - vector.Y - 2f + num3 - Main.screenPosition.Y;
						}
						else
						{
							num4 = num8;
							num8 = (float)num9 / num8;
							vector.X = (float)(Main.screenWidth / 2) + num6 * num8 - vector.X / 2f;
							vector.Y = (float)(Main.screenHeight / 2) + num7 * num8;
						}
						if (num4 > 0f)
						{
							string text2 = "(" + (int)(num4 / 16f * 2f) + " ft)";
							Vector2 vector3 = Main.fontMouseText.MeasureString(text2);
							vector3.X = vector.X + Main.fontMouseText.MeasureString(text).X / 2f - vector3.X / 2f;
							vector3.Y = vector.Y + Main.fontMouseText.MeasureString(text).Y / 2f - vector3.Y / 2f - 20f;
							SpriteBatch arg_5B5_0 = this.spriteBatch;
							SpriteFont arg_5B5_1 = Main.fontMouseText;
							string arg_5B5_2 = text2;
							Vector2 arg_5B5_3 = new Vector2(vector3.X - 2f, vector3.Y);
							Color arg_5B5_4 = Color.Black;
							float arg_5B5_5 = 0f;
							origin = default(Vector2);
							arg_5B5_0.DrawString(arg_5B5_1, arg_5B5_2, arg_5B5_3, arg_5B5_4, arg_5B5_5, origin, 1f, SpriteEffects.None, 0f);
							SpriteBatch arg_603_0 = this.spriteBatch;
							SpriteFont arg_603_1 = Main.fontMouseText;
							string arg_603_2 = text2;
							Vector2 arg_603_3 = new Vector2(vector3.X + 2f, vector3.Y);
							Color arg_603_4 = Color.Black;
							float arg_603_5 = 0f;
							origin = default(Vector2);
							arg_603_0.DrawString(arg_603_1, arg_603_2, arg_603_3, arg_603_4, arg_603_5, origin, 1f, SpriteEffects.None, 0f);
							SpriteBatch arg_651_0 = this.spriteBatch;
							SpriteFont arg_651_1 = Main.fontMouseText;
							string arg_651_2 = text2;
							Vector2 arg_651_3 = new Vector2(vector3.X, vector3.Y - 2f);
							Color arg_651_4 = Color.Black;
							float arg_651_5 = 0f;
							origin = default(Vector2);
							arg_651_0.DrawString(arg_651_1, arg_651_2, arg_651_3, arg_651_4, arg_651_5, origin, 1f, SpriteEffects.None, 0f);
							SpriteBatch arg_69F_0 = this.spriteBatch;
							SpriteFont arg_69F_1 = Main.fontMouseText;
							string arg_69F_2 = text2;
							Vector2 arg_69F_3 = new Vector2(vector3.X, vector3.Y + 2f);
							Color arg_69F_4 = Color.Black;
							float arg_69F_5 = 0f;
							origin = default(Vector2);
							arg_69F_0.DrawString(arg_69F_1, arg_69F_2, arg_69F_3, arg_69F_4, arg_69F_5, origin, 1f, SpriteEffects.None, 0f);
							SpriteBatch arg_6D3_0 = this.spriteBatch;
							SpriteFont arg_6D3_1 = Main.fontMouseText;
							string arg_6D3_2 = text2;
							Vector2 arg_6D3_3 = vector3;
							Color arg_6D3_4 = color;
							float arg_6D3_5 = 0f;
							origin = default(Vector2);
							arg_6D3_0.DrawString(arg_6D3_1, arg_6D3_2, arg_6D3_3, arg_6D3_4, arg_6D3_5, origin, 1f, SpriteEffects.None, 0f);
						}
						SpriteBatch arg_721_0 = this.spriteBatch;
						SpriteFont arg_721_1 = Main.fontMouseText;
						string arg_721_2 = text;
						Vector2 arg_721_3 = new Vector2(vector.X - 2f, vector.Y);
						Color arg_721_4 = Color.Black;
						float arg_721_5 = 0f;
						origin = default(Vector2);
						arg_721_0.DrawString(arg_721_1, arg_721_2, arg_721_3, arg_721_4, arg_721_5, origin, 1f, SpriteEffects.None, 0f);
						SpriteBatch arg_76F_0 = this.spriteBatch;
						SpriteFont arg_76F_1 = Main.fontMouseText;
						string arg_76F_2 = text;
						Vector2 arg_76F_3 = new Vector2(vector.X + 2f, vector.Y);
						Color arg_76F_4 = Color.Black;
						float arg_76F_5 = 0f;
						origin = default(Vector2);
						arg_76F_0.DrawString(arg_76F_1, arg_76F_2, arg_76F_3, arg_76F_4, arg_76F_5, origin, 1f, SpriteEffects.None, 0f);
						SpriteBatch arg_7BD_0 = this.spriteBatch;
						SpriteFont arg_7BD_1 = Main.fontMouseText;
						string arg_7BD_2 = text;
						Vector2 arg_7BD_3 = new Vector2(vector.X, vector.Y - 2f);
						Color arg_7BD_4 = Color.Black;
						float arg_7BD_5 = 0f;
						origin = default(Vector2);
						arg_7BD_0.DrawString(arg_7BD_1, arg_7BD_2, arg_7BD_3, arg_7BD_4, arg_7BD_5, origin, 1f, SpriteEffects.None, 0f);
						SpriteBatch arg_80B_0 = this.spriteBatch;
						SpriteFont arg_80B_1 = Main.fontMouseText;
						string arg_80B_2 = text;
						Vector2 arg_80B_3 = new Vector2(vector.X, vector.Y + 2f);
						Color arg_80B_4 = Color.Black;
						float arg_80B_5 = 0f;
						origin = default(Vector2);
						arg_80B_0.DrawString(arg_80B_1, arg_80B_2, arg_80B_3, arg_80B_4, arg_80B_5, origin, 1f, SpriteEffects.None, 0f);
						SpriteBatch arg_83F_0 = this.spriteBatch;
						SpriteFont arg_83F_1 = Main.fontMouseText;
						string arg_83F_2 = text;
						Vector2 arg_83F_3 = vector;
						Color arg_83F_4 = color;
						float arg_83F_5 = 0f;
						origin = default(Vector2);
						arg_83F_0.DrawString(arg_83F_1, arg_83F_2, arg_83F_3, arg_83F_4, arg_83F_5, origin, 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.playerInventory)
			{
				Main.npcChatText = "";
				Main.player[Main.myPlayer].sign = -1;
			}
			if (Main.ignoreErrors)
			{
				try
				{
					if (Main.npcChatText != "" || Main.player[Main.myPlayer].sign != -1)
					{
						this.DrawChat();
					}
					goto IL_8D5;
				}
				catch
				{
					goto IL_8D5;
				}
			}
			if (Main.npcChatText != "" || Main.player[Main.myPlayer].sign != -1)
			{
				this.DrawChat();
			}
			IL_8D5:
			Color color2 = new Color(220, 220, 220, 220);
			Main.invAlpha += Main.invDir * 0.2f;
			if (Main.invAlpha > 240f)
			{
				Main.invAlpha = 240f;
				Main.invDir = -1f;
			}
			if (Main.invAlpha < 180f)
			{
				Main.invAlpha = 180f;
				Main.invDir = 1f;
			}
			color2 = new Color((int)((byte)Main.invAlpha), (int)((byte)Main.invAlpha), (int)((byte)Main.invAlpha), (int)((byte)Main.invAlpha));
			bool flag = false;
			int rare = 0;
			int num10 = Main.screenWidth - 800;
			int num11 = Main.player[Main.myPlayer].statLifeMax / 20;
			if (num11 >= 10)
			{
				num11 = 10;
			}
			string text3 = string.Concat(new object[]
			{
				"Life: ", 
				Main.player[Main.myPlayer].statLifeMax, 
				"/", 
				Main.player[Main.myPlayer].statLifeMax
			});
			Vector2 vector4 = Main.fontMouseText.MeasureString(text3);
			if (!Main.player[Main.myPlayer].ghost)
			{
				SpriteBatch arg_A8A_0 = this.spriteBatch;
				SpriteFont arg_A8A_1 = Main.fontMouseText;
				string arg_A8A_2 = "Life: ";
				Vector2 arg_A8A_3 = new Vector2((float)(500 + 13 * num11) - vector4.X * 0.5f + (float)num10, 6f);
				Color arg_A8A_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_A8A_5 = 0f;
				origin = default(Vector2);
				arg_A8A_0.DrawString(arg_A8A_1, arg_A8A_2, arg_A8A_3, arg_A8A_4, arg_A8A_5, origin, 1f, SpriteEffects.None, 0f);
				this.spriteBatch.DrawString(Main.fontMouseText, Main.player[Main.myPlayer].statLife + "/" + Main.player[Main.myPlayer].statLifeMax, new Vector2((float)(500 + 13 * num11) + vector4.X * 0.5f + (float)num10, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, new Vector2(Main.fontMouseText.MeasureString(Main.player[Main.myPlayer].statLife + "/" + Main.player[Main.myPlayer].statLifeMax).X, 0f), 1f, SpriteEffects.None, 0f);
			}
			int num12 = 20;
			for (int j = 1; j < Main.player[Main.myPlayer].statLifeMax / num12 + 1; j++)
			{
				int num13 = 255;
				float num14 = 1f;
				bool flag2 = false;
				if (Main.player[Main.myPlayer].statLife >= j * num12)
				{
					num13 = 255;
					if (Main.player[Main.myPlayer].statLife == j * num12)
					{
						flag2 = true;
					}
				}
				else
				{
					float num15 = (float)(Main.player[Main.myPlayer].statLife - (j - 1) * num12) / (float)num12;
					num13 = (int)(30f + 225f * num15);
					if (num13 < 30)
					{
						num13 = 30;
					}
					num14 = num15 / 4f + 0.75f;
					if ((double)num14 < 0.75)
					{
						num14 = 0.75f;
					}
					if (num15 > 0f)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					num14 += Main.cursorScale - 1f;
				}
				int num16 = 0;
				int num17 = 0;
				if (j > 10)
				{
					num16 -= 260;
					num17 += 26;
				}
				int a = (int)((double)((float)num13) * 0.9);
				if (!Main.player[Main.myPlayer].ghost)
				{
					this.spriteBatch.Draw(Main.heartTexture, new Vector2((float)(500 + 26 * (j - 1) + num16 + num10 + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * num14) / 2f + (float)num17 + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(num13, num13, num13, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), num14, SpriteEffects.None, 0f);
				}
			}
			int num18 = 20;
			if (Main.player[Main.myPlayer].statManaMax2 > 0)
			{
				int arg_DA0_0 = Main.player[Main.myPlayer].statManaMax2 / 20;
				SpriteBatch arg_DFB_0 = this.spriteBatch;
				SpriteFont arg_DFB_1 = Main.fontMouseText;
				string arg_DFB_2 = "Mana";
				Vector2 arg_DFB_3 = new Vector2((float)(750 + num10), 6f);
				Color arg_DFB_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_DFB_5 = 0f;
				origin = default(Vector2);
				arg_DFB_0.DrawString(arg_DFB_1, arg_DFB_2, arg_DFB_3, arg_DFB_4, arg_DFB_5, origin, 1f, SpriteEffects.None, 0f);
				for (int k = 1; k < Main.player[Main.myPlayer].statManaMax2 / num18 + 1; k++)
				{
					int num19 = 255;
					bool flag3 = false;
					float num20 = 1f;
					if (Main.player[Main.myPlayer].statMana >= k * num18)
					{
						num19 = 255;
						if (Main.player[Main.myPlayer].statMana == k * num18)
						{
							flag3 = true;
						}
					}
					else
					{
						float num21 = (float)(Main.player[Main.myPlayer].statMana - (k - 1) * num18) / (float)num18;
						num19 = (int)(30f + 225f * num21);
						if (num19 < 30)
						{
							num19 = 30;
						}
						num20 = num21 / 4f + 0.75f;
						if ((double)num20 < 0.75)
						{
							num20 = 0.75f;
						}
						if (num21 > 0f)
						{
							flag3 = true;
						}
					}
					if (flag3)
					{
						num20 += Main.cursorScale - 1f;
					}
					int a2 = (int)((double)((float)num19) * 0.9);
					this.spriteBatch.Draw(Main.manaTexture, new Vector2((float)(775 + num10), (float)(30 + Main.manaTexture.Height / 2) + ((float)Main.manaTexture.Height - (float)Main.manaTexture.Height * num20) / 2f + (float)(28 * (k - 1))), new Rectangle?(new Rectangle(0, 0, Main.manaTexture.Width, Main.manaTexture.Height)), new Color(num19, num19, num19, a2), 0f, new Vector2((float)(Main.manaTexture.Width / 2), (float)(Main.manaTexture.Height / 2)), num20, SpriteEffects.None, 0f);
				}
			}
			if (Main.player[Main.myPlayer].breath < Main.player[Main.myPlayer].breathMax && !Main.player[Main.myPlayer].ghost)
			{
				int num22 = 76;
				int arg_1007_0 = Main.player[Main.myPlayer].breathMax / 20;
				SpriteBatch arg_1084_0 = this.spriteBatch;
				SpriteFont arg_1084_1 = Main.fontMouseText;
				string arg_1084_2 = "Breath";
				Vector2 arg_1084_3 = new Vector2((float)(500 + 13 * num11) - Main.fontMouseText.MeasureString("Breath").X * 0.5f + (float)num10, (float)(6 + num22));
				Color arg_1084_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_1084_5 = 0f;
				origin = default(Vector2);
				arg_1084_0.DrawString(arg_1084_1, arg_1084_2, arg_1084_3, arg_1084_4, arg_1084_5, origin, 1f, SpriteEffects.None, 0f);
				int num23 = 20;
				for (int l = 1; l < Main.player[Main.myPlayer].breathMax / num23 + 1; l++)
				{
					int num24 = 255;
					float num25 = 1f;
					if (Main.player[Main.myPlayer].breath >= l * num23)
					{
						num24 = 255;
					}
					else
					{
						float num26 = (float)(Main.player[Main.myPlayer].breath - (l - 1) * num23) / (float)num23;
						num24 = (int)(30f + 225f * num26);
						if (num24 < 30)
						{
							num24 = 30;
						}
						num25 = num26 / 4f + 0.75f;
						if ((double)num25 < 0.75)
						{
							num25 = 0.75f;
						}
					}
					int num27 = 0;
					int num28 = 0;
					if (l > 10)
					{
						num27 -= 260;
						num28 += 26;
					}
					SpriteBatch arg_11D9_0 = this.spriteBatch;
					Texture2D arg_11D9_1 = Main.bubbleTexture;
					Vector2 arg_11D9_2 = new Vector2((float)(500 + 26 * (l - 1) + num27 + num10), 32f + ((float)Main.bubbleTexture.Height - (float)Main.bubbleTexture.Height * num25) / 2f + (float)num28 + (float)num22);
					Rectangle? arg_11D9_3 = new Rectangle?(new Rectangle(0, 0, Main.bubbleTexture.Width, Main.bubbleTexture.Height));
					Color arg_11D9_4 = new Color(num24, num24, num24, num24);
					float arg_11D9_5 = 0f;
					origin = default(Vector2);
					arg_11D9_0.Draw(arg_11D9_1, arg_11D9_2, arg_11D9_3, arg_11D9_4, arg_11D9_5, origin, num25, SpriteEffects.None, 0f);
				}
			}
			Main.buffString = "";
			if (!Main.playerInventory)
			{
				int num29 = -1;
				for (int m = 0; m < 10; m++)
				{
					if (Main.player[Main.myPlayer].buffType[m] > 0)
					{
						int num30 = Main.player[Main.myPlayer].buffType[m];
						int num31 = 32 + m * 38;
						int num32 = 76;
						Color color3 = new Color(Main.buffAlpha[m], Main.buffAlpha[m], Main.buffAlpha[m], Main.buffAlpha[m]);
						SpriteBatch arg_12E1_0 = this.spriteBatch;
						Texture2D arg_12E1_1 = Main.buffTexture[num30];
						Vector2 arg_12E1_2 = new Vector2((float)num31, (float)num32);
						Rectangle? arg_12E1_3 = new Rectangle?(new Rectangle(0, 0, Main.buffTexture[num30].Width, Main.buffTexture[num30].Height));
						Color arg_12E1_4 = color3;
						float arg_12E1_5 = 0f;
						origin = default(Vector2);
						arg_12E1_0.Draw(arg_12E1_1, arg_12E1_2, arg_12E1_3, arg_12E1_4, arg_12E1_5, origin, 1f, SpriteEffects.None, 0f);
						string text4 = "0 s";
						if (Main.player[Main.myPlayer].buffTime[m] / 60 >= 60)
						{
							text4 = Math.Round((double)(Main.player[Main.myPlayer].buffTime[m] / 60) / 60.0) + " m";
						}
						else
						{
							text4 = Math.Round((double)Main.player[Main.myPlayer].buffTime[m] / 60.0) + " s";
						}
						SpriteBatch arg_13BA_0 = this.spriteBatch;
						SpriteFont arg_13BA_1 = Main.fontItemStack;
						string arg_13BA_2 = text4;
						Vector2 arg_13BA_3 = new Vector2((float)num31, (float)(num32 + Main.buffTexture[num30].Height));
						Color arg_13BA_4 = color3;
						float arg_13BA_5 = 0f;
						origin = default(Vector2);
						arg_13BA_0.DrawString(arg_13BA_1, arg_13BA_2, arg_13BA_3, arg_13BA_4, arg_13BA_5, origin, 0.8f, SpriteEffects.None, 0f);
						if (Main.mouseState.X < num31 + Main.buffTexture[num30].Width && Main.mouseState.Y < num32 + Main.buffTexture[num30].Height && Main.mouseState.X > num31 && Main.mouseState.Y > num32)
						{
							num29 = m;
							Main.buffAlpha[m] += 0.1f;
							if (Main.mouseState.RightButton == ButtonState.Pressed && Main.mouseRightRelease && !Main.debuff[num30])
							{
								Main.PlaySound(12, -1, -1, 1);
								Main.player[Main.myPlayer].DelBuff(m);
							}
						}
						else
						{
							Main.buffAlpha[m] -= 0.05f;
						}
						if (Main.buffAlpha[m] > 1f)
						{
							Main.buffAlpha[m] = 1f;
						}
						else
						{
							if ((double)Main.buffAlpha[m] < 0.4)
							{
								Main.buffAlpha[m] = 0.4f;
							}
						}
					}
					else
					{
						Main.buffAlpha[m] = 0.4f;
					}
				}
				if (num29 >= 0)
				{
					int num33 = Main.player[Main.myPlayer].buffType[num29];
					if (num33 > 0)
					{
						Main.buffString = Main.buffTip[num33];
						this.MouseText(Main.buffName[num33], 0, 0);
					}
				}
			}
			if (Main.player[Main.myPlayer].dead)
			{
				Main.playerInventory = false;
			}
			if (!Main.playerInventory)
			{
				Main.player[Main.myPlayer].chest = -1;
				Main.craftGuide = false;
			}
			string text5 = "";
			if (Main.playerInventory)
			{
				if (Main.netMode == 1)
				{
					int num34 = 675 + Main.screenWidth - 800;
					int num35 = 114;
					if (Main.player[Main.myPlayer].hostile)
					{
						SpriteBatch arg_1622_0 = this.spriteBatch;
						Texture2D arg_1622_1 = Main.itemTexture[4];
						Vector2 arg_1622_2 = new Vector2((float)(num34 - 2), (float)num35);
						Rectangle? arg_1622_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height));
						Color arg_1622_4 = Main.teamColor[Main.player[Main.myPlayer].team];
						float arg_1622_5 = 0f;
						origin = default(Vector2);
						arg_1622_0.Draw(arg_1622_1, arg_1622_2, arg_1622_3, arg_1622_4, arg_1622_5, origin, 1f, SpriteEffects.None, 0f);
						SpriteBatch arg_16A2_0 = this.spriteBatch;
						Texture2D arg_16A2_1 = Main.itemTexture[4];
						Vector2 arg_16A2_2 = new Vector2((float)(num34 + 2), (float)num35);
						Rectangle? arg_16A2_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height));
						Color arg_16A2_4 = Main.teamColor[Main.player[Main.myPlayer].team];
						float arg_16A2_5 = 0f;
						origin = default(Vector2);
						arg_16A2_0.Draw(arg_16A2_1, arg_16A2_2, arg_16A2_3, arg_16A2_4, arg_16A2_5, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
					}
					else
					{
						SpriteBatch arg_172B_0 = this.spriteBatch;
						Texture2D arg_172B_1 = Main.itemTexture[4];
						Vector2 arg_172B_2 = new Vector2((float)(num34 - 16), (float)(num35 + 14));
						Rectangle? arg_172B_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height));
						Color arg_172B_4 = Main.teamColor[Main.player[Main.myPlayer].team];
						float arg_172B_5 = -0.785f;
						origin = default(Vector2);
						arg_172B_0.Draw(arg_172B_1, arg_172B_2, arg_172B_3, arg_172B_4, arg_172B_5, origin, 1f, SpriteEffects.None, 0f);
						SpriteBatch arg_17AE_0 = this.spriteBatch;
						Texture2D arg_17AE_1 = Main.itemTexture[4];
						Vector2 arg_17AE_2 = new Vector2((float)(num34 + 2), (float)(num35 + 14));
						Rectangle? arg_17AE_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height));
						Color arg_17AE_4 = Main.teamColor[Main.player[Main.myPlayer].team];
						float arg_17AE_5 = -0.785f;
						origin = default(Vector2);
						arg_17AE_0.Draw(arg_17AE_1, arg_17AE_2, arg_17AE_3, arg_17AE_4, arg_17AE_5, origin, 1f, SpriteEffects.None, 0f);
					}
					if (Main.mouseState.X > num34 && Main.mouseState.X < num34 + 34 && Main.mouseState.Y > num35 - 2 && Main.mouseState.Y < num35 + 34)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease)
						{
							if (Main.teamCooldown == 0)
							{
								Main.teamCooldown = Main.teamCooldownLen;
								Main.PlaySound(12, -1, -1, 1);
								if (Main.player[Main.myPlayer].hostile)
								{
									Main.player[Main.myPlayer].hostile = false;
								}
								else
								{
									Main.player[Main.myPlayer].hostile = true;
								}
								NetMessage.SendData(30, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
							}
							else
							{
								Main.NewText("You must wait " + (Main.teamCooldown / 60 + 1) + " seconds.", 255, 0, 0);
							}
						}
					}
					num34 -= 3;
					Rectangle value = new Rectangle(Main.mouseState.X, Main.mouseState.Y, 1, 1);
					int width = Main.teamTexture.Width;
					int height = Main.teamTexture.Height;
					for (int n = 0; n < 5; n++)
					{
						Rectangle rectangle = default(Rectangle);
						if (n == 0)
						{
							rectangle = new Rectangle(num34 + 50, num35 - 20, width, height);
						}
						if (n == 1)
						{
							rectangle = new Rectangle(num34 + 40, num35, width, height);
						}
						if (n == 2)
						{
							rectangle = new Rectangle(num34 + 60, num35, width, height);
						}
						if (n == 3)
						{
							rectangle = new Rectangle(num34 + 40, num35 + 20, width, height);
						}
						if (n == 4)
						{
							rectangle = new Rectangle(num34 + 60, num35 + 20, width, height);
						}
						if (rectangle.Intersects(value))
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease && Main.player[Main.myPlayer].team != n)
							{
								if (Main.teamCooldown == 0)
								{
									Main.teamCooldown = Main.teamCooldownLen;
									Main.PlaySound(12, -1, -1, 1);
									Main.player[Main.myPlayer].team = n;
									NetMessage.SendData(45, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
								}
								else
								{
									Main.NewText("You must wait " + (Main.teamCooldown / 60 + 1) + " seconds.", 255, 0, 0);
								}
							}
						}
					}
					SpriteBatch arg_1AD1_0 = this.spriteBatch;
					Texture2D arg_1AD1_1 = Main.teamTexture;
					Vector2 arg_1AD1_2 = new Vector2((float)(num34 + 50), (float)(num35 - 20));
					Rectangle? arg_1AD1_3 = new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height));
					Color arg_1AD1_4 = Main.teamColor[0];
					float arg_1AD1_5 = 0f;
					origin = default(Vector2);
					arg_1AD1_0.Draw(arg_1AD1_1, arg_1AD1_2, arg_1AD1_3, arg_1AD1_4, arg_1AD1_5, origin, 1f, SpriteEffects.None, 0f);
					SpriteBatch arg_1B3D_0 = this.spriteBatch;
					Texture2D arg_1B3D_1 = Main.teamTexture;
					Vector2 arg_1B3D_2 = new Vector2((float)(num34 + 40), (float)num35);
					Rectangle? arg_1B3D_3 = new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height));
					Color arg_1B3D_4 = Main.teamColor[1];
					float arg_1B3D_5 = 0f;
					origin = default(Vector2);
					arg_1B3D_0.Draw(arg_1B3D_1, arg_1B3D_2, arg_1B3D_3, arg_1B3D_4, arg_1B3D_5, origin, 1f, SpriteEffects.None, 0f);
					SpriteBatch arg_1BA9_0 = this.spriteBatch;
					Texture2D arg_1BA9_1 = Main.teamTexture;
					Vector2 arg_1BA9_2 = new Vector2((float)(num34 + 60), (float)num35);
					Rectangle? arg_1BA9_3 = new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height));
					Color arg_1BA9_4 = Main.teamColor[2];
					float arg_1BA9_5 = 0f;
					origin = default(Vector2);
					arg_1BA9_0.Draw(arg_1BA9_1, arg_1BA9_2, arg_1BA9_3, arg_1BA9_4, arg_1BA9_5, origin, 1f, SpriteEffects.None, 0f);
					SpriteBatch arg_1C18_0 = this.spriteBatch;
					Texture2D arg_1C18_1 = Main.teamTexture;
					Vector2 arg_1C18_2 = new Vector2((float)(num34 + 40), (float)(num35 + 20));
					Rectangle? arg_1C18_3 = new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height));
					Color arg_1C18_4 = Main.teamColor[3];
					float arg_1C18_5 = 0f;
					origin = default(Vector2);
					arg_1C18_0.Draw(arg_1C18_1, arg_1C18_2, arg_1C18_3, arg_1C18_4, arg_1C18_5, origin, 1f, SpriteEffects.None, 0f);
					SpriteBatch arg_1C87_0 = this.spriteBatch;
					Texture2D arg_1C87_1 = Main.teamTexture;
					Vector2 arg_1C87_2 = new Vector2((float)(num34 + 60), (float)(num35 + 20));
					Rectangle? arg_1C87_3 = new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height));
					Color arg_1C87_4 = Main.teamColor[4];
					float arg_1C87_5 = 0f;
					origin = default(Vector2);
					arg_1C87_0.Draw(arg_1C87_1, arg_1C87_2, arg_1C87_3, arg_1C87_4, arg_1C87_5, origin, 1f, SpriteEffects.None, 0f);
				}
				bool flag4 = false;
				if (Main.keyState.IsKeyDown(Keys.LeftShift))
				{
					flag4 = true;
				}
				Main.inventoryScale = 0.85f;
				int num36 = 448;
				int num37 = 210;
				Color white = new Color(150, 150, 150, 150);
				if (Main.mouseState.X >= num36 && (float)Main.mouseState.X <= (float)num36 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num37 && (float)Main.mouseState.Y <= (float)num37 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
					{
						if (Main.mouseItem.type != 0)
						{
							Main.trashItem.SetDefaults(0, false);
						}
						Item item = Main.mouseItem;
						Main.mouseItem = Main.trashItem;
						Main.trashItem = item;
						if (Main.trashItem.type == 0 || Main.trashItem.stack < 1)
						{
							Main.trashItem = new Item();
						}
						if (Main.mouseItem.IsTheSameAs(Main.trashItem) && Main.trashItem.stack != Main.trashItem.maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
						{
							if (Main.mouseItem.stack + Main.trashItem.stack <= Main.mouseItem.maxStack)
							{
								Main.trashItem.stack += Main.mouseItem.stack;
								Main.mouseItem.stack = 0;
							}
							else
							{
								int num38 = Main.mouseItem.maxStack - Main.trashItem.stack;
								Main.trashItem.stack += num38;
								Main.mouseItem.stack -= num38;
							}
						}
						if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
						{
							Main.mouseItem = new Item();
						}
						if (Main.mouseItem.type > 0 || Main.trashItem.type > 0)
						{
							Main.PlaySound(7, -1, -1, 1);
						}
					}
					if (!flag4)
					{
						text5 = Main.trashItem.name;
						if (Main.trashItem.stack > 1)
						{
							object obj = text5;
							text5 = string.Concat(new object[]
							{
								obj, 
								" (", 
								Main.trashItem.stack, 
								")"
							});
						}
						Main.toolTip = (Item)Main.trashItem.Clone();
						if (text5 == null)
						{
							text5 = "Trash Can";
						}
					}
					else
					{
						text5 = "Trash Can";
					}
				}
				SpriteBatch arg_1FC0_0 = this.spriteBatch;
				Texture2D arg_1FC0_1 = Main.inventoryBack7Texture;
				Vector2 arg_1FC0_2 = new Vector2((float)num36, (float)num37);
				Rectangle? arg_1FC0_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
				Color arg_1FC0_4 = color2;
				float arg_1FC0_5 = 0f;
				origin = default(Vector2);
				arg_1FC0_0.Draw(arg_1FC0_1, arg_1FC0_2, arg_1FC0_3, arg_1FC0_4, arg_1FC0_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
				white = Color.White;
				if (Main.trashItem.type == 0 || Main.trashItem.stack == 0 || flag4)
				{
					white = new Color(100, 100, 100, 100);
					float num39 = Main.inventoryScale;
					SpriteBatch arg_2096_0 = this.spriteBatch;
					Texture2D arg_2096_1 = Main.trashTexture;
					Vector2 arg_2096_2 = new Vector2((float)num36 + 26f * Main.inventoryScale - (float)Main.trashTexture.Width * 0.5f * num39, (float)num37 + 26f * Main.inventoryScale - (float)Main.trashTexture.Height * 0.5f * num39);
					Rectangle? arg_2096_3 = new Rectangle?(new Rectangle(0, 0, Main.trashTexture.Width, Main.trashTexture.Height));
					Color arg_2096_4 = white;
					float arg_2096_5 = 0f;
					origin = default(Vector2);
					arg_2096_0.Draw(arg_2096_1, arg_2096_2, arg_2096_3, arg_2096_4, arg_2096_5, origin, num39, SpriteEffects.None, 0f);
				}
				else
				{
					float num40 = 1f;
					if (Main.itemTexture[Main.trashItem.type].Width > 32 || Main.itemTexture[Main.trashItem.type].Height > 32)
					{
						if (Main.itemTexture[Main.trashItem.type].Width > Main.itemTexture[Main.trashItem.type].Height)
						{
							num40 = 32f / (float)Main.itemTexture[Main.trashItem.type].Width;
						}
						else
						{
							num40 = 32f / (float)Main.itemTexture[Main.trashItem.type].Height;
						}
					}
					num40 *= Main.inventoryScale;
					SpriteBatch arg_2223_0 = this.spriteBatch;
					Texture2D arg_2223_1 = Main.itemTexture[Main.trashItem.type];
					Vector2 arg_2223_2 = new Vector2((float)num36 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Width * 0.5f * num40, (float)num37 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Height * 0.5f * num40);
					Rectangle? arg_2223_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.trashItem.type].Width, Main.itemTexture[Main.trashItem.type].Height));
					Color arg_2223_4 = Main.trashItem.GetAlpha(white);
					float arg_2223_5 = 0f;
					origin = default(Vector2);
					arg_2223_0.Draw(arg_2223_1, arg_2223_2, arg_2223_3, arg_2223_4, arg_2223_5, origin, num40, SpriteEffects.None, 0f);
					Color arg_2240_0 = Main.trashItem.color;
					Color b = default(Color);
					if (arg_2240_0 != b)
					{
						SpriteBatch arg_2320_0 = this.spriteBatch;
						Texture2D arg_2320_1 = Main.itemTexture[Main.trashItem.type];
						Vector2 arg_2320_2 = new Vector2((float)num36 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Width * 0.5f * num40, (float)num37 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.trashItem.type].Height * 0.5f * num40);
						Rectangle? arg_2320_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.trashItem.type].Width, Main.itemTexture[Main.trashItem.type].Height));
						Color arg_2320_4 = Main.trashItem.GetColor(white);
						float arg_2320_5 = 0f;
						origin = default(Vector2);
						arg_2320_0.Draw(arg_2320_1, arg_2320_2, arg_2320_3, arg_2320_4, arg_2320_5, origin, num40, SpriteEffects.None, 0f);
					}
					if (Main.trashItem.stack > 1)
					{
						SpriteBatch arg_2391_0 = this.spriteBatch;
						SpriteFont arg_2391_1 = Main.fontItemStack;
						string arg_2391_2 = string.Concat(Main.trashItem.stack);
						Vector2 arg_2391_3 = new Vector2((float)num36 + 10f * Main.inventoryScale, (float)num37 + 26f * Main.inventoryScale);
						Color arg_2391_4 = white;
						float arg_2391_5 = 0f;
						origin = default(Vector2);
						arg_2391_0.DrawString(arg_2391_1, arg_2391_2, arg_2391_3, arg_2391_4, arg_2391_5, origin, num40, SpriteEffects.None, 0f);
					}
				}
				SpriteBatch arg_23EC_0 = this.spriteBatch;
				SpriteFont arg_23EC_1 = Main.fontMouseText;
				string arg_23EC_2 = "Inventory";
				Vector2 arg_23EC_3 = new Vector2(40f, 0f);
				Color arg_23EC_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_23EC_5 = 0f;
				origin = default(Vector2);
				arg_23EC_0.DrawString(arg_23EC_1, arg_23EC_2, arg_23EC_3, arg_23EC_4, arg_23EC_5, origin, 1f, SpriteEffects.None, 0f);
				Main.inventoryScale = 0.85f;
				if (Main.mouseState.X > 20 && Main.mouseState.X < (int)(20f + 560f * Main.inventoryScale) && Main.mouseState.Y > 20 && Main.mouseState.Y < (int)(20f + 224f * Main.inventoryScale))
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				for (int num41 = 0; num41 < 10; num41++)
				{
					for (int num42 = 0; num42 < 4; num42++)
					{
						int num43 = (int)(20f + (float)(num41 * 56) * Main.inventoryScale);
						int num44 = (int)(20f + (float)(num42 * 56) * Main.inventoryScale);
						int num45 = num41 + num42 * 10;
						Color white2 = new Color(100, 100, 100, 100);
						if (Main.mouseState.X >= num43 && (float)Main.mouseState.X <= (float)num43 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num44 && (float)Main.mouseState.Y <= (float)num44 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
							{
								if (Main.keyState.IsKeyDown(Keys.LeftShift))
								{
									if (Main.player[Main.myPlayer].inventory[num45].type > 0)
									{
										if (Main.npcShop > 0)
										{
											if (Main.player[Main.myPlayer].SellItem(Main.player[Main.myPlayer].inventory[num45].value, Main.player[Main.myPlayer].inventory[num45].stack))
											{
												this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num45]);
												Main.player[Main.myPlayer].inventory[num45].SetDefaults(0, false);
												Main.PlaySound(18, -1, -1, 1);
											}
											else
											{
												if (Main.mouseItem.value == 0)
												{
													this.shop[Main.npcShop].AddShop(Main.player[Main.myPlayer].inventory[num45]);
													Main.player[Main.myPlayer].inventory[num45].SetDefaults(0, false);
													Main.PlaySound(7, -1, -1, 1);
												}
											}
										}
										else
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
											Main.trashItem = (Item)Main.player[Main.myPlayer].inventory[num45].Clone();
											Main.player[Main.myPlayer].inventory[num45].SetDefaults(0, false);
										}
									}
								}
								else
								{
									if (Main.player[Main.myPlayer].selectedItem != num45 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item2 = Main.mouseItem;
										Main.mouseItem = Main.player[Main.myPlayer].inventory[num45];
										Main.player[Main.myPlayer].inventory[num45] = item2;
										if (Main.player[Main.myPlayer].inventory[num45].type == 0 || Main.player[Main.myPlayer].inventory[num45].stack < 1)
										{
											Main.player[Main.myPlayer].inventory[num45] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num45]) && Main.player[Main.myPlayer].inventory[num45].stack != Main.player[Main.myPlayer].inventory[num45].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[num45].stack <= Main.mouseItem.maxStack)
											{
												Main.player[Main.myPlayer].inventory[num45].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num46 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[num45].stack;
												Main.player[Main.myPlayer].inventory[num45].stack += num46;
												Main.mouseItem.stack -= num46;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[num45].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
								}
							}
							else
							{
								if (Main.mouseState.RightButton == ButtonState.Pressed && Main.mouseRightRelease && Main.player[Main.myPlayer].inventory[num45].maxStack == 1)
								{
									Main.player[Main.myPlayer].inventory[num45] = Main.armorSwap(Main.player[Main.myPlayer].inventory[num45]);
								}
								else
								{
									if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && Main.player[Main.myPlayer].inventory[num45].maxStack > 1 && Main.player[Main.myPlayer].inventory[num45].type > 0 && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num45]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
									{
										if (Main.mouseItem.type == 0)
										{
											Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[num45].Clone();
											Main.mouseItem.stack = 0;
										}
										Main.mouseItem.stack++;
										Main.player[Main.myPlayer].inventory[num45].stack--;
										if (Main.player[Main.myPlayer].inventory[num45].stack <= 0)
										{
											Main.player[Main.myPlayer].inventory[num45] = new Item();
										}
										Recipe.FindRecipes();
										Main.soundInstanceMenuTick.Stop();
										Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
										Main.PlaySound(12, -1, -1, 1);
										if (Main.stackSplit == 0)
										{
											Main.stackSplit = 15;
										}
										else
										{
											Main.stackSplit = Main.stackDelay;
										}
									}
								}
							}
							text5 = Main.player[Main.myPlayer].inventory[num45].name;
							Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[num45].Clone();
							if (Main.player[Main.myPlayer].inventory[num45].stack > 1)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj, 
									" (", 
									Main.player[Main.myPlayer].inventory[num45].stack, 
									")"
								});
							}
						}
						if (num42 != 0)
						{
							SpriteBatch arg_2BF6_0 = this.spriteBatch;
							Texture2D arg_2BF6_1 = Main.inventoryBackTexture;
							Vector2 arg_2BF6_2 = new Vector2((float)num43, (float)num44);
							Rectangle? arg_2BF6_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_2BF6_4 = color2;
							float arg_2BF6_5 = 0f;
							origin = default(Vector2);
							arg_2BF6_0.Draw(arg_2BF6_1, arg_2BF6_2, arg_2BF6_3, arg_2BF6_4, arg_2BF6_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
						}
						else
						{
							SpriteBatch arg_2C53_0 = this.spriteBatch;
							Texture2D arg_2C53_1 = Main.inventoryBack9Texture;
							Vector2 arg_2C53_2 = new Vector2((float)num43, (float)num44);
							Rectangle? arg_2C53_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_2C53_4 = color2;
							float arg_2C53_5 = 0f;
							origin = default(Vector2);
							arg_2C53_0.Draw(arg_2C53_1, arg_2C53_2, arg_2C53_3, arg_2C53_4, arg_2C53_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
						}
						white2 = Color.White;
						if (Main.player[Main.myPlayer].inventory[num45].type > 0 && Main.player[Main.myPlayer].inventory[num45].stack > 0)
						{
							float num47 = 1f;
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height > 32)
							{
								if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height)
								{
									num47 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width;
								}
								else
								{
									num47 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height;
								}
							}
							num47 *= Main.inventoryScale;
							SpriteBatch arg_2EC9_0 = this.spriteBatch;
							Texture2D arg_2EC9_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type];
							Vector2 arg_2EC9_2 = new Vector2((float)num43 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width * 0.5f * num47, (float)num44 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height * 0.5f * num47);
							Rectangle? arg_2EC9_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height));
							Color arg_2EC9_4 = Main.player[Main.myPlayer].inventory[num45].GetAlpha(white2);
							float arg_2EC9_5 = 0f;
							origin = default(Vector2);
							arg_2EC9_0.Draw(arg_2EC9_1, arg_2EC9_2, arg_2EC9_3, arg_2EC9_4, arg_2EC9_5, origin, num47, SpriteEffects.None, 0f);
							Color arg_2EF4_0 = Main.player[Main.myPlayer].inventory[num45].color;
							Color b = default(Color);
							if (arg_2EF4_0 != b)
							{
								SpriteBatch arg_3028_0 = this.spriteBatch;
								Texture2D arg_3028_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type];
								Vector2 arg_3028_2 = new Vector2((float)num43 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width * 0.5f * num47, (float)num44 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height * 0.5f * num47);
								Rectangle? arg_3028_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num45].type].Height));
								Color arg_3028_4 = Main.player[Main.myPlayer].inventory[num45].GetColor(white2);
								float arg_3028_5 = 0f;
								origin = default(Vector2);
								arg_3028_0.Draw(arg_3028_1, arg_3028_2, arg_3028_3, arg_3028_4, arg_3028_5, origin, num47, SpriteEffects.None, 0f);
							}
							if (Main.player[Main.myPlayer].inventory[num45].stack > 1)
							{
								SpriteBatch arg_30B5_0 = this.spriteBatch;
								SpriteFont arg_30B5_1 = Main.fontItemStack;
								string arg_30B5_2 = string.Concat(Main.player[Main.myPlayer].inventory[num45].stack);
								Vector2 arg_30B5_3 = new Vector2((float)num43 + 10f * Main.inventoryScale, (float)num44 + 26f * Main.inventoryScale);
								Color arg_30B5_4 = white2;
								float arg_30B5_5 = 0f;
								origin = default(Vector2);
								arg_30B5_0.DrawString(arg_30B5_1, arg_30B5_2, arg_30B5_3, arg_30B5_4, arg_30B5_5, origin, num47, SpriteEffects.None, 0f);
							}
						}
						if (num42 == 0)
						{
							string text6 = string.Concat(num45 + 1);
							if (text6 == "10")
							{
								text6 = "0";
							}
							SpriteBatch arg_3125_0 = this.spriteBatch;
							SpriteFont arg_3125_1 = Main.fontItemStack;
							string arg_3125_2 = text6;
							Vector2 arg_3125_3 = new Vector2((float)(num43 + 6), (float)(num44 + 4));
							Color arg_3125_4 = color2;
							float arg_3125_5 = 0f;
							origin = default(Vector2);
							arg_3125_0.DrawString(arg_3125_1, arg_3125_2, arg_3125_3, arg_3125_4, arg_3125_5, origin, Main.inventoryScale * 0.8f, SpriteEffects.None, 0f);
						}
					}
				}
				int num48 = 0;
				int num49 = 2;
				int num50 = 32;
				if (!Main.player[Main.myPlayer].hbLocked)
				{
					num48 = 1;
				}
				SpriteBatch arg_31C5_0 = this.spriteBatch;
				Texture2D arg_31C5_1 = Main.HBLockTexture[num48];
				Vector2 arg_31C5_2 = new Vector2((float)num49, (float)num50);
				Rectangle? arg_31C5_3 = new Rectangle?(new Rectangle(0, 0, Main.HBLockTexture[num48].Width, Main.HBLockTexture[num48].Height));
				Color arg_31C5_4 = color2;
				float arg_31C5_5 = 0f;
				origin = default(Vector2);
				arg_31C5_0.Draw(arg_31C5_1, arg_31C5_2, arg_31C5_3, arg_31C5_4, arg_31C5_5, origin, 0.9f, SpriteEffects.None, 0f);
				if (Main.mouseState.X > num49 && (float)Main.mouseState.X < (float)num49 + (float)Main.HBLockTexture[num48].Width * 0.9f && Main.mouseState.Y > num50 && (float)Main.mouseState.Y < (float)num50 + (float)Main.HBLockTexture[num48].Height * 0.9f)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					if (!Main.player[Main.myPlayer].hbLocked)
					{
						this.MouseText("Hotbar unlocked", 0, 0);
						flag = true;
					}
					else
					{
						this.MouseText("Hotbar locked", 0, 0);
						flag = true;
					}
					if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease)
					{
						Main.PlaySound(22, -1, -1, 1);
						if (!Main.player[Main.myPlayer].hbLocked)
						{
							Main.player[Main.myPlayer].hbLocked = true;
						}
						else
						{
							Main.player[Main.myPlayer].hbLocked = false;
						}
					}
				}
				if (Main.armorHide)
				{
					Main.armorAlpha -= 0.1f;
					if (Main.armorAlpha < 0f)
					{
						Main.armorAlpha = 0f;
					}
				}
				else
				{
					Main.armorAlpha += 0.025f;
					if (Main.armorAlpha > 1f)
					{
						Main.armorAlpha = 1f;
					}
				}
				Color color4 = new Color((int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.armorAlpha)));
				Main.armorHide = false;
				SpriteBatch arg_33CA_0 = this.spriteBatch;
				SpriteFont arg_33CA_1 = Main.fontMouseText;
				string arg_33CA_2 = "Equip";
				Vector2 arg_33CA_3 = new Vector2((float)(Main.screenWidth - 64 - 28 + 4), 152f);
				Color arg_33CA_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_33CA_5 = 0f;
				origin = default(Vector2);
				arg_33CA_0.DrawString(arg_33CA_1, arg_33CA_2, arg_33CA_3, arg_33CA_4, arg_33CA_5, origin, 0.8f, SpriteEffects.None, 0f);
				if (Main.mouseState.X > Main.screenWidth - 64 - 28 && Main.mouseState.X < (int)((float)(Main.screenWidth - 64 - 28) + 56f * Main.inventoryScale) && Main.mouseState.Y > 174 && Main.mouseState.Y < (int)(174f + 448f * Main.inventoryScale))
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				for (int num51 = 0; num51 < 8; num51++)
				{
					int num52 = Main.screenWidth - 64 - 28;
					int num53 = (int)(174f + (float)(num51 * 56) * Main.inventoryScale);
					Color white3 = new Color(100, 100, 100, 100);
					string text7 = "";
					if (num51 == 3)
					{
						text7 = "Accessories";
					}
					if (num51 == 7)
					{
						text7 = Main.player[Main.myPlayer].statDefense + " Defense";
					}
					Vector2 vector5 = Main.fontMouseText.MeasureString(text7);
					SpriteBatch arg_3531_0 = this.spriteBatch;
					SpriteFont arg_3531_1 = Main.fontMouseText;
					string arg_3531_2 = text7;
					Vector2 arg_3531_3 = new Vector2((float)num52 - vector5.X - 10f, (float)num53 + (float)Main.inventoryBackTexture.Height * 0.5f - vector5.Y * 0.5f);
					Color arg_3531_4 = color4;
					float arg_3531_5 = 0f;
					origin = default(Vector2);
					arg_3531_0.DrawString(arg_3531_1, arg_3531_2, arg_3531_3, arg_3531_4, arg_3531_5, origin, 1f, SpriteEffects.None, 0f);
					if (Main.mouseState.X >= num52 && (float)Main.mouseState.X <= (float)num52 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num53 && (float)Main.mouseState.Y <= (float)num53 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.armorHide = true;
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && (Main.mouseItem.type == 0 || (Main.mouseItem.headSlot > -1 && num51 == 0) || (Main.mouseItem.bodySlot > -1 && num51 == 1) || (Main.mouseItem.legSlot > -1 && num51 == 2) || (Main.mouseItem.accessory && num51 > 2 && !Main.AccCheck(Main.mouseItem))))
						{
							Item item3 = Main.mouseItem;
							Main.mouseItem = Main.player[Main.myPlayer].armor[num51];
							Main.player[Main.myPlayer].armor[num51] = item3;
							if (Main.player[Main.myPlayer].armor[num51].type == 0 || Main.player[Main.myPlayer].armor[num51].stack < 1)
							{
								Main.player[Main.myPlayer].armor[num51] = new Item();
							}
							if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
							{
								Main.mouseItem = new Item();
							}
							if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].armor[num51].type > 0)
							{
								Recipe.FindRecipes();
								Main.PlaySound(7, -1, -1, 1);
							}
						}
						text5 = Main.player[Main.myPlayer].armor[num51].name;
						Main.toolTip = (Item)Main.player[Main.myPlayer].armor[num51].Clone();
						if (num51 <= 2)
						{
							Main.toolTip.wornArmor = true;
						}
						if (Main.player[Main.myPlayer].armor[num51].stack > 1)
						{
							object obj = text5;
							text5 = string.Concat(new object[]
							{
								obj, 
								" (", 
								Main.player[Main.myPlayer].armor[num51].stack, 
								")"
							});
						}
					}
					SpriteBatch arg_382F_0 = this.spriteBatch;
					Texture2D arg_382F_1 = Main.inventoryBack3Texture;
					Vector2 arg_382F_2 = new Vector2((float)num52, (float)num53);
					Rectangle? arg_382F_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
					Color arg_382F_4 = color2;
					float arg_382F_5 = 0f;
					origin = default(Vector2);
					arg_382F_0.Draw(arg_382F_1, arg_382F_2, arg_382F_3, arg_382F_4, arg_382F_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
					white3 = Color.White;
					if (Main.player[Main.myPlayer].armor[num51].type > 0 && Main.player[Main.myPlayer].armor[num51].stack > 0)
					{
						float num54 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width > Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height)
							{
								num54 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width;
							}
							else
							{
								num54 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height;
							}
						}
						num54 *= Main.inventoryScale;
						SpriteBatch arg_3AA5_0 = this.spriteBatch;
						Texture2D arg_3AA5_1 = Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type];
						Vector2 arg_3AA5_2 = new Vector2((float)num52 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width * 0.5f * num54, (float)num53 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height * 0.5f * num54);
						Rectangle? arg_3AA5_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height));
						Color arg_3AA5_4 = Main.player[Main.myPlayer].armor[num51].GetAlpha(white3);
						float arg_3AA5_5 = 0f;
						origin = default(Vector2);
						arg_3AA5_0.Draw(arg_3AA5_1, arg_3AA5_2, arg_3AA5_3, arg_3AA5_4, arg_3AA5_5, origin, num54, SpriteEffects.None, 0f);
						Color arg_3AD0_0 = Main.player[Main.myPlayer].armor[num51].color;
						Color b = default(Color);
						if (arg_3AD0_0 != b)
						{
							SpriteBatch arg_3C04_0 = this.spriteBatch;
							Texture2D arg_3C04_1 = Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type];
							Vector2 arg_3C04_2 = new Vector2((float)num52 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width * 0.5f * num54, (float)num53 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height * 0.5f * num54);
							Rectangle? arg_3C04_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num51].type].Height));
							Color arg_3C04_4 = Main.player[Main.myPlayer].armor[num51].GetColor(white3);
							float arg_3C04_5 = 0f;
							origin = default(Vector2);
							arg_3C04_0.Draw(arg_3C04_1, arg_3C04_2, arg_3C04_3, arg_3C04_4, arg_3C04_5, origin, num54, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].armor[num51].stack > 1)
						{
							SpriteBatch arg_3C91_0 = this.spriteBatch;
							SpriteFont arg_3C91_1 = Main.fontItemStack;
							string arg_3C91_2 = string.Concat(Main.player[Main.myPlayer].armor[num51].stack);
							Vector2 arg_3C91_3 = new Vector2((float)num52 + 10f * Main.inventoryScale, (float)num53 + 26f * Main.inventoryScale);
							Color arg_3C91_4 = white3;
							float arg_3C91_5 = 0f;
							origin = default(Vector2);
							arg_3C91_0.DrawString(arg_3C91_1, arg_3C91_2, arg_3C91_3, arg_3C91_4, arg_3C91_5, origin, num54, SpriteEffects.None, 0f);
						}
					}
				}
				SpriteBatch arg_3D04_0 = this.spriteBatch;
				SpriteFont arg_3D04_1 = Main.fontMouseText;
				string arg_3D04_2 = "Social";
				Vector2 arg_3D04_3 = new Vector2((float)(Main.screenWidth - 64 - 28 - 44), 152f);
				Color arg_3D04_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_3D04_5 = 0f;
				origin = default(Vector2);
				arg_3D04_0.DrawString(arg_3D04_1, arg_3D04_2, arg_3D04_3, arg_3D04_4, arg_3D04_5, origin, 0.8f, SpriteEffects.None, 0f);
				if (Main.mouseState.X > Main.screenWidth - 64 - 28 - 47 && Main.mouseState.X < (int)((float)(Main.screenWidth - 64 - 20 - 47) + 56f * Main.inventoryScale) && Main.mouseState.Y > 174 && Main.mouseState.Y < (int)(174f + 168f * Main.inventoryScale))
				{
					Main.player[Main.myPlayer].mouseInterface = true;
				}
				for (int num55 = 8; num55 < 11; num55++)
				{
					int num56 = Main.screenWidth - 64 - 28 - 47;
					int num57 = (int)(174f + (float)((num55 - 8) * 56) * Main.inventoryScale);
					Color white4 = new Color(100, 100, 100, 100);
					string text8 = "";
					if (num55 == 8)
					{
						text8 = "Helmet";
					}
					else
					{
						if (num55 == 9)
						{
							text8 = "Shirt";
						}
						else
						{
							if (num55 == 10)
							{
								text8 = "Pants";
							}
						}
					}
					Vector2 vector6 = Main.fontMouseText.MeasureString(text8);
					SpriteBatch arg_3E6E_0 = this.spriteBatch;
					SpriteFont arg_3E6E_1 = Main.fontMouseText;
					string arg_3E6E_2 = text8;
					Vector2 arg_3E6E_3 = new Vector2((float)num56 - vector6.X - 10f, (float)num57 + (float)Main.inventoryBackTexture.Height * 0.5f - vector6.Y * 0.5f);
					Color arg_3E6E_4 = color4;
					float arg_3E6E_5 = 0f;
					origin = default(Vector2);
					arg_3E6E_0.DrawString(arg_3E6E_1, arg_3E6E_2, arg_3E6E_3, arg_3E6E_4, arg_3E6E_5, origin, 1f, SpriteEffects.None, 0f);
					if (Main.mouseState.X >= num56 && (float)Main.mouseState.X <= (float)num56 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num57 && (float)Main.mouseState.Y <= (float)num57 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						Main.armorHide = true;
						if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
						{
							if (Main.mouseItem.type == 0 || (Main.mouseItem.headSlot > -1 && num55 == 8) || (Main.mouseItem.bodySlot > -1 && num55 == 9) || (Main.mouseItem.legSlot > -1 && num55 == 10))
							{
								Item item4 = Main.mouseItem;
								Main.mouseItem = Main.player[Main.myPlayer].armor[num55];
								Main.player[Main.myPlayer].armor[num55] = item4;
								if (Main.player[Main.myPlayer].armor[num55].type == 0 || Main.player[Main.myPlayer].armor[num55].stack < 1)
								{
									Main.player[Main.myPlayer].armor[num55] = new Item();
								}
								if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
								{
									Main.mouseItem = new Item();
								}
								if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].armor[num55].type > 0)
								{
									Recipe.FindRecipes();
									Main.PlaySound(7, -1, -1, 1);
								}
							}
						}
						else
						{
							if (Main.mouseState.RightButton == ButtonState.Pressed && Main.mouseRightRelease && Main.player[Main.myPlayer].armor[num55].maxStack == 1)
							{
								Main.player[Main.myPlayer].armor[num55] = Main.armorSwap(Main.player[Main.myPlayer].armor[num55]);
							}
						}
						text5 = Main.player[Main.myPlayer].armor[num55].name;
						Main.toolTip = (Item)Main.player[Main.myPlayer].armor[num55].Clone();
						Main.toolTip.social = true;
						if (num55 <= 2)
						{
							Main.toolTip.wornArmor = true;
						}
						if (Main.player[Main.myPlayer].armor[num55].stack > 1)
						{
							object obj = text5;
							text5 = string.Concat(new object[]
							{
								obj, 
								" (", 
								Main.player[Main.myPlayer].armor[num55].stack, 
								")"
							});
						}
					}
					SpriteBatch arg_41B6_0 = this.spriteBatch;
					Texture2D arg_41B6_1 = Main.inventoryBack8Texture;
					Vector2 arg_41B6_2 = new Vector2((float)num56, (float)num57);
					Rectangle? arg_41B6_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
					Color arg_41B6_4 = color2;
					float arg_41B6_5 = 0f;
					origin = default(Vector2);
					arg_41B6_0.Draw(arg_41B6_1, arg_41B6_2, arg_41B6_3, arg_41B6_4, arg_41B6_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
					white4 = Color.White;
					if (Main.player[Main.myPlayer].armor[num55].type > 0 && Main.player[Main.myPlayer].armor[num55].stack > 0)
					{
						float num58 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width > Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height)
							{
								num58 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width;
							}
							else
							{
								num58 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height;
							}
						}
						num58 *= Main.inventoryScale;
						SpriteBatch arg_442C_0 = this.spriteBatch;
						Texture2D arg_442C_1 = Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type];
						Vector2 arg_442C_2 = new Vector2((float)num56 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width * 0.5f * num58, (float)num57 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height * 0.5f * num58);
						Rectangle? arg_442C_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height));
						Color arg_442C_4 = Main.player[Main.myPlayer].armor[num55].GetAlpha(white4);
						float arg_442C_5 = 0f;
						origin = default(Vector2);
						arg_442C_0.Draw(arg_442C_1, arg_442C_2, arg_442C_3, arg_442C_4, arg_442C_5, origin, num58, SpriteEffects.None, 0f);
						Color arg_4457_0 = Main.player[Main.myPlayer].armor[num55].color;
						Color b = default(Color);
						if (arg_4457_0 != b)
						{
							SpriteBatch arg_458B_0 = this.spriteBatch;
							Texture2D arg_458B_1 = Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type];
							Vector2 arg_458B_2 = new Vector2((float)num56 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width * 0.5f * num58, (float)num57 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height * 0.5f * num58);
							Rectangle? arg_458B_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[num55].type].Height));
							Color arg_458B_4 = Main.player[Main.myPlayer].armor[num55].GetColor(white4);
							float arg_458B_5 = 0f;
							origin = default(Vector2);
							arg_458B_0.Draw(arg_458B_1, arg_458B_2, arg_458B_3, arg_458B_4, arg_458B_5, origin, num58, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].armor[num55].stack > 1)
						{
							SpriteBatch arg_4618_0 = this.spriteBatch;
							SpriteFont arg_4618_1 = Main.fontItemStack;
							string arg_4618_2 = string.Concat(Main.player[Main.myPlayer].armor[num55].stack);
							Vector2 arg_4618_3 = new Vector2((float)num56 + 10f * Main.inventoryScale, (float)num57 + 26f * Main.inventoryScale);
							Color arg_4618_4 = white4;
							float arg_4618_5 = 0f;
							origin = default(Vector2);
							arg_4618_0.DrawString(arg_4618_1, arg_4618_2, arg_4618_3, arg_4618_4, arg_4618_5, origin, num58, SpriteEffects.None, 0f);
						}
					}
				}
				if (Main.craftingHide)
				{
					Main.craftingAlpha -= 0.1f;
					if (Main.craftingAlpha < 0f)
					{
						Main.craftingAlpha = 0f;
					}
				}
				else
				{
					Main.craftingAlpha += 0.025f;
					if (Main.craftingAlpha > 1f)
					{
						Main.craftingAlpha = 1f;
					}
				}
				Color color5 = new Color((int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)), (int)((byte)((float)Main.mouseTextColor * Main.craftingAlpha)));
				Main.craftingHide = false;
				if (Main.craftGuide)
				{
					if (Main.player[Main.myPlayer].chest != -1 || Main.npcShop != 0 || Main.player[Main.myPlayer].talkNPC == -1)
					{
						Main.craftGuide = false;
						Main.player[Main.myPlayer].dropItemCheck();
						Recipe.FindRecipes();
					}
					else
					{
						int num59 = 73;
						int num60 = 331;
						string text9;
						if (Main.guideItem.type > 0)
						{
							text9 = "Showing recipes that use " + Main.guideItem.name;
							SpriteBatch arg_4788_0 = this.spriteBatch;
							SpriteFont arg_4788_1 = Main.fontMouseText;
							string arg_4788_2 = "Required objects:";
							Vector2 arg_4788_3 = new Vector2((float)num59, (float)(num60 + 118));
							Color arg_4788_4 = color5;
							float arg_4788_5 = 0f;
							origin = default(Vector2);
							arg_4788_0.DrawString(arg_4788_1, arg_4788_2, arg_4788_3, arg_4788_4, arg_4788_5, origin, 1f, SpriteEffects.None, 0f);
							int num61 = Main.focusRecipe;
							int num62 = 0;
							int num63 = 0;
							while (num63 < Recipe.maxRequirements)
							{
								int num64 = (num63 + 1) * 26;
								if (Main.recipe[Main.availableRecipe[num61]].requiredTile[num63] == -1)
								{
									if (num63 == 0 && !Main.recipe[Main.availableRecipe[num61]].needWater)
									{
										SpriteBatch arg_4821_0 = this.spriteBatch;
										SpriteFont arg_4821_1 = Main.fontMouseText;
										string arg_4821_2 = "None";
										Vector2 arg_4821_3 = new Vector2((float)num59, (float)(num60 + 118 + num64));
										Color arg_4821_4 = color5;
										float arg_4821_5 = 0f;
										origin = default(Vector2);
										arg_4821_0.DrawString(arg_4821_1, arg_4821_2, arg_4821_3, arg_4821_4, arg_4821_5, origin, 1f, SpriteEffects.None, 0f);
										break;
									}
									break;
								}
								else
								{
									num62++;
									SpriteBatch arg_4886_0 = this.spriteBatch;
									SpriteFont arg_4886_1 = Main.fontMouseText;
									string arg_4886_2 = Main.tileName[Main.recipe[Main.availableRecipe[num61]].requiredTile[num63]];
									Vector2 arg_4886_3 = new Vector2((float)num59, (float)(num60 + 118 + num64));
									Color arg_4886_4 = color5;
									float arg_4886_5 = 0f;
									origin = default(Vector2);
									arg_4886_0.DrawString(arg_4886_1, arg_4886_2, arg_4886_3, arg_4886_4, arg_4886_5, origin, 1f, SpriteEffects.None, 0f);
									num63++;
								}
							}
							if (Main.recipe[Main.availableRecipe[num61]].needWater)
							{
								int num65 = (num62 + 1) * 26;
								SpriteBatch arg_48FC_0 = this.spriteBatch;
								SpriteFont arg_48FC_1 = Main.fontMouseText;
								string arg_48FC_2 = "Water";
								Vector2 arg_48FC_3 = new Vector2((float)num59, (float)(num60 + 118 + num65));
								Color arg_48FC_4 = color5;
								float arg_48FC_5 = 0f;
								origin = default(Vector2);
								arg_48FC_0.DrawString(arg_48FC_1, arg_48FC_2, arg_48FC_3, arg_48FC_4, arg_48FC_5, origin, 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							text9 = "Place a material here to show recipes";
						}
						SpriteBatch arg_495F_0 = this.spriteBatch;
						SpriteFont arg_495F_1 = Main.fontMouseText;
						string arg_495F_2 = text9;
						Vector2 arg_495F_3 = new Vector2((float)(num59 + 50), (float)(num60 + 12));
						Color arg_495F_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
						float arg_495F_5 = 0f;
						origin = default(Vector2);
						arg_495F_0.DrawString(arg_495F_1, arg_495F_2, arg_495F_3, arg_495F_4, arg_495F_5, origin, 1f, SpriteEffects.None, 0f);
						Color white5 = new Color(100, 100, 100, 100);
						if (Main.mouseState.X >= num59 && (float)Main.mouseState.X <= (float)num59 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num60 && (float)Main.mouseState.Y <= (float)num60 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							Main.craftingHide = true;
							if (Main.mouseItem.material || Main.mouseItem.type == 0)
							{
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									Item item5 = Main.mouseItem;
									Main.mouseItem = Main.guideItem;
									Main.guideItem = item5;
									if (Main.guideItem.type == 0 || Main.guideItem.stack < 1)
									{
										Main.guideItem = new Item();
									}
									if (Main.mouseItem.IsTheSameAs(Main.guideItem) && Main.guideItem.stack != Main.guideItem.maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
									{
										if (Main.mouseItem.stack + Main.guideItem.stack <= Main.mouseItem.maxStack)
										{
											Main.guideItem.stack += Main.mouseItem.stack;
											Main.mouseItem.stack = 0;
										}
										else
										{
											int num66 = Main.mouseItem.maxStack - Main.guideItem.stack;
											Main.guideItem.stack += num66;
											Main.mouseItem.stack -= num66;
										}
									}
									if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
									{
										Main.mouseItem = new Item();
									}
									if (Main.mouseItem.type > 0 || Main.guideItem.type > 0)
									{
										Recipe.FindRecipes();
										Main.PlaySound(7, -1, -1, 1);
									}
								}
								else
								{
									if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.guideItem) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
									{
										if (Main.mouseItem.type == 0)
										{
											Main.mouseItem = (Item)Main.guideItem.Clone();
											Main.mouseItem.stack = 0;
										}
										Main.mouseItem.stack++;
										Main.guideItem.stack--;
										if (Main.guideItem.stack <= 0)
										{
											Main.guideItem = new Item();
										}
										Recipe.FindRecipes();
										Main.soundInstanceMenuTick.Stop();
										Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
										Main.PlaySound(12, -1, -1, 1);
										if (Main.stackSplit == 0)
										{
											Main.stackSplit = 15;
										}
										else
										{
											Main.stackSplit = Main.stackDelay;
										}
									}
								}
							}
							text5 = Main.guideItem.name;
							Main.toolTip = (Item)Main.guideItem.Clone();
							if (Main.guideItem.stack > 1)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj, 
									" (", 
									Main.guideItem.stack, 
									")"
								});
							}
						}
						SpriteBatch arg_4D60_0 = this.spriteBatch;
						Texture2D arg_4D60_1 = Main.inventoryBack4Texture;
						Vector2 arg_4D60_2 = new Vector2((float)num59, (float)num60);
						Rectangle? arg_4D60_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
						Color arg_4D60_4 = color2;
						float arg_4D60_5 = 0f;
						origin = default(Vector2);
						arg_4D60_0.Draw(arg_4D60_1, arg_4D60_2, arg_4D60_3, arg_4D60_4, arg_4D60_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
						white5 = Color.White;
						if (Main.guideItem.type > 0 && Main.guideItem.stack > 0)
						{
							float num67 = 1f;
							if (Main.itemTexture[Main.guideItem.type].Width > 32 || Main.itemTexture[Main.guideItem.type].Height > 32)
							{
								if (Main.itemTexture[Main.guideItem.type].Width > Main.itemTexture[Main.guideItem.type].Height)
								{
									num67 = 32f / (float)Main.itemTexture[Main.guideItem.type].Width;
								}
								else
								{
									num67 = 32f / (float)Main.itemTexture[Main.guideItem.type].Height;
								}
							}
							num67 *= Main.inventoryScale;
							SpriteBatch arg_4F0F_0 = this.spriteBatch;
							Texture2D arg_4F0F_1 = Main.itemTexture[Main.guideItem.type];
							Vector2 arg_4F0F_2 = new Vector2((float)num59 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Width * 0.5f * num67, (float)num60 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Height * 0.5f * num67);
							Rectangle? arg_4F0F_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.guideItem.type].Width, Main.itemTexture[Main.guideItem.type].Height));
							Color arg_4F0F_4 = Main.guideItem.GetAlpha(white5);
							float arg_4F0F_5 = 0f;
							origin = default(Vector2);
							arg_4F0F_0.Draw(arg_4F0F_1, arg_4F0F_2, arg_4F0F_3, arg_4F0F_4, arg_4F0F_5, origin, num67, SpriteEffects.None, 0f);
							Color arg_4F2C_0 = Main.guideItem.color;
							Color b = default(Color);
							if (arg_4F2C_0 != b)
							{
								SpriteBatch arg_500C_0 = this.spriteBatch;
								Texture2D arg_500C_1 = Main.itemTexture[Main.guideItem.type];
								Vector2 arg_500C_2 = new Vector2((float)num59 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Width * 0.5f * num67, (float)num60 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.guideItem.type].Height * 0.5f * num67);
								Rectangle? arg_500C_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.guideItem.type].Width, Main.itemTexture[Main.guideItem.type].Height));
								Color arg_500C_4 = Main.guideItem.GetColor(white5);
								float arg_500C_5 = 0f;
								origin = default(Vector2);
								arg_500C_0.Draw(arg_500C_1, arg_500C_2, arg_500C_3, arg_500C_4, arg_500C_5, origin, num67, SpriteEffects.None, 0f);
							}
							if (Main.guideItem.stack > 1)
							{
								SpriteBatch arg_507D_0 = this.spriteBatch;
								SpriteFont arg_507D_1 = Main.fontItemStack;
								string arg_507D_2 = string.Concat(Main.guideItem.stack);
								Vector2 arg_507D_3 = new Vector2((float)num59 + 10f * Main.inventoryScale, (float)num60 + 26f * Main.inventoryScale);
								Color arg_507D_4 = white5;
								float arg_507D_5 = 0f;
								origin = default(Vector2);
								arg_507D_0.DrawString(arg_507D_1, arg_507D_2, arg_507D_3, arg_507D_4, arg_507D_5, origin, num67, SpriteEffects.None, 0f);
							}
						}
					}
				}
				if (Main.numAvailableRecipes > 0)
				{
					SpriteBatch arg_50C9_0 = this.spriteBatch;
					SpriteFont arg_50C9_1 = Main.fontMouseText;
					string arg_50C9_2 = "Crafting";
					Vector2 arg_50C9_3 = new Vector2(76f, 414f);
					Color arg_50C9_4 = color5;
					float arg_50C9_5 = 0f;
					origin = default(Vector2);
					arg_50C9_0.DrawString(arg_50C9_1, arg_50C9_2, arg_50C9_3, arg_50C9_4, arg_50C9_5, origin, 1f, SpriteEffects.None, 0f);
				}
				for (int num68 = 0; num68 < Recipe.maxRecipes; num68++)
				{
					Main.inventoryScale = 100f / (Math.Abs(Main.availableRecipeY[num68]) + 100f);
					if ((double)Main.inventoryScale < 0.75)
					{
						Main.inventoryScale = 0.75f;
					}
					if (Main.availableRecipeY[num68] < (float)((num68 - Main.focusRecipe) * 65))
					{
						if (Main.availableRecipeY[num68] == 0f)
						{
							Main.PlaySound(12, -1, -1, 1);
						}
						Main.availableRecipeY[num68] += 6.5f;
					}
					else
					{
						if (Main.availableRecipeY[num68] > (float)((num68 - Main.focusRecipe) * 65))
						{
							if (Main.availableRecipeY[num68] == 0f)
							{
								Main.PlaySound(12, -1, -1, 1);
							}
							Main.availableRecipeY[num68] -= 6.5f;
						}
					}
					if (num68 < Main.numAvailableRecipes && Math.Abs(Main.availableRecipeY[num68]) <= 250f)
					{
						int num69 = (int)(46f - 26f * Main.inventoryScale);
						int num70 = (int)(410f + Main.availableRecipeY[num68] * Main.inventoryScale - 30f * Main.inventoryScale);
						double num71 = (double)(color2.A + 50);
						double num72 = 255.0;
						if (Math.Abs(Main.availableRecipeY[num68]) > 150f)
						{
							num71 = (double)(150f * (100f - (Math.Abs(Main.availableRecipeY[num68]) - 150f))) * 0.01;
							num72 = (double)(255f * (100f - (Math.Abs(Main.availableRecipeY[num68]) - 150f))) * 0.01;
						}
						new Color((int)((byte)num71), (int)((byte)num71), (int)((byte)num71), (int)((byte)num71));
						Color color6 = new Color((int)((byte)num72), (int)((byte)num72), (int)((byte)num72), (int)((byte)num72));
						if (Main.mouseState.X >= num69 && (float)Main.mouseState.X <= (float)num69 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num70 && (float)Main.mouseState.Y <= (float)num70 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							if (Main.focusRecipe == num68 && Main.guideItem.type == 0)
							{
								if (Main.mouseItem.type == 0 || (Main.mouseItem.IsTheSameAs(Main.recipe[Main.availableRecipe[num68]].createItem) && Main.mouseItem.stack + Main.recipe[Main.availableRecipe[num68]].createItem.stack <= Main.mouseItem.maxStack))
								{
									if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
									{
										int stack = Main.mouseItem.stack;
										Main.mouseItem = (Item)Main.recipe[Main.availableRecipe[num68]].createItem.Clone();
										Main.mouseItem.stack += stack;
										Main.recipe[Main.availableRecipe[num68]].Create();
										if (Main.mouseItem.type > 0 || Main.recipe[Main.availableRecipe[num68]].createItem.type > 0)
										{
											Main.PlaySound(7, -1, -1, 1);
										}
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
											int stack2 = Main.mouseItem.stack;
											Main.mouseItem = (Item)Main.recipe[Main.availableRecipe[num68]].createItem.Clone();
											Main.mouseItem.stack += stack2;
											Main.recipe[Main.availableRecipe[num68]].Create();
											if (Main.mouseItem.type > 0 || Main.recipe[Main.availableRecipe[num68]].createItem.type > 0)
											{
												Main.PlaySound(7, -1, -1, 1);
											}
										}
									}
								}
							}
							else
							{
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									Main.focusRecipe = num68;
								}
							}
							Main.craftingHide = true;
							text5 = Main.recipe[Main.availableRecipe[num68]].createItem.name;
							Main.toolTip = (Item)Main.recipe[Main.availableRecipe[num68]].createItem.Clone();
							if (Main.recipe[Main.availableRecipe[num68]].createItem.stack > 1)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj, 
									" (", 
									Main.recipe[Main.availableRecipe[num68]].createItem.stack, 
									")"
								});
							}
						}
						if (Main.numAvailableRecipes > 0)
						{
							num71 -= 50.0;
							if (num71 < 0.0)
							{
								num71 = 0.0;
							}
							SpriteBatch arg_5698_0 = this.spriteBatch;
							Texture2D arg_5698_1 = Main.inventoryBack4Texture;
							Vector2 arg_5698_2 = new Vector2((float)num69, (float)num70);
							Rectangle? arg_5698_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_5698_4 = new Color((int)((byte)num71), (int)((byte)num71), (int)((byte)num71), (int)((byte)num71));
							float arg_5698_5 = 0f;
							origin = default(Vector2);
							arg_5698_0.Draw(arg_5698_1, arg_5698_2, arg_5698_3, arg_5698_4, arg_5698_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
							if (Main.recipe[Main.availableRecipe[num68]].createItem.type > 0 && Main.recipe[Main.availableRecipe[num68]].createItem.stack > 0)
							{
								float num73 = 1f;
								if (Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width > 32 || Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height > 32)
								{
									if (Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width > Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height)
									{
										num73 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width;
									}
									else
									{
										num73 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height;
									}
								}
								num73 *= Main.inventoryScale;
								SpriteBatch arg_5907_0 = this.spriteBatch;
								Texture2D arg_5907_1 = Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type];
								Vector2 arg_5907_2 = new Vector2((float)num69 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width * 0.5f * num73, (float)num70 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height * 0.5f * num73);
								Rectangle? arg_5907_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height));
								Color arg_5907_4 = Main.recipe[Main.availableRecipe[num68]].createItem.GetAlpha(color6);
								float arg_5907_5 = 0f;
								origin = default(Vector2);
								arg_5907_0.Draw(arg_5907_1, arg_5907_2, arg_5907_3, arg_5907_4, arg_5907_5, origin, num73, SpriteEffects.None, 0f);
								Color arg_5932_0 = Main.recipe[Main.availableRecipe[num68]].createItem.color;
								Color b = default(Color);
								if (arg_5932_0 != b)
								{
									SpriteBatch arg_5A66_0 = this.spriteBatch;
									Texture2D arg_5A66_1 = Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type];
									Vector2 arg_5A66_2 = new Vector2((float)num69 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width * 0.5f * num73, (float)num70 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height * 0.5f * num73);
									Rectangle? arg_5A66_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[num68]].createItem.type].Height));
									Color arg_5A66_4 = Main.recipe[Main.availableRecipe[num68]].createItem.GetColor(color6);
									float arg_5A66_5 = 0f;
									origin = default(Vector2);
									arg_5A66_0.Draw(arg_5A66_1, arg_5A66_2, arg_5A66_3, arg_5A66_4, arg_5A66_5, origin, num73, SpriteEffects.None, 0f);
								}
								if (Main.recipe[Main.availableRecipe[num68]].createItem.stack > 1)
								{
									SpriteBatch arg_5AF3_0 = this.spriteBatch;
									SpriteFont arg_5AF3_1 = Main.fontItemStack;
									string arg_5AF3_2 = string.Concat(Main.recipe[Main.availableRecipe[num68]].createItem.stack);
									Vector2 arg_5AF3_3 = new Vector2((float)num69 + 10f * Main.inventoryScale, (float)num70 + 26f * Main.inventoryScale);
									Color arg_5AF3_4 = color6;
									float arg_5AF3_5 = 0f;
									origin = default(Vector2);
									arg_5AF3_0.DrawString(arg_5AF3_1, arg_5AF3_2, arg_5AF3_3, arg_5AF3_4, arg_5AF3_5, origin, num73, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.numAvailableRecipes > 0)
				{
					int num74 = 0;
					while (num74 < Recipe.maxRequirements && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type != 0)
					{
						int num75 = 80 + num74 * 40;
						int num76 = 380;
						double num77 = (double)(color2.A + 50);
						double num78 = 255.0;
						Color white6 = Color.White;
						Color white7 = Color.White;
						num77 = (double)((float)(color2.A + 50) - Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2f);
						num78 = (double)(255f - Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2f);
						if (num77 < 0.0)
						{
							num77 = 0.0;
						}
						if (num78 < 0.0)
						{
							num78 = 0.0;
						}
						white6.R = (byte)num77;
						white6.G = (byte)num77;
						white6.B = (byte)num77;
						white6.A = (byte)num77;
						white7.R = (byte)num78;
						white7.G = (byte)num78;
						white7.B = (byte)num78;
						white7.A = (byte)num78;
						Main.inventoryScale = 0.6f;
						if (num77 == 0.0)
						{
							break;
						}
						if (Main.mouseState.X >= num75 && (float)Main.mouseState.X <= (float)num75 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num76 && (float)Main.mouseState.Y <= (float)num76 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
						{
							Main.craftingHide = true;
							Main.player[Main.myPlayer].mouseInterface = true;
							text5 = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].name;
							Main.toolTip = (Item)Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].Clone();
							if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].stack > 1)
							{
								object obj = text5;
								text5 = string.Concat(new object[]
								{
									obj, 
									" (", 
									Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].stack, 
									")"
								});
							}
						}
						num77 -= 50.0;
						if (num77 < 0.0)
						{
							num77 = 0.0;
						}
						SpriteBatch arg_5E30_0 = this.spriteBatch;
						Texture2D arg_5E30_1 = Main.inventoryBack4Texture;
						Vector2 arg_5E30_2 = new Vector2((float)num75, (float)num76);
						Rectangle? arg_5E30_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
						Color arg_5E30_4 = new Color((int)((byte)num77), (int)((byte)num77), (int)((byte)num77), (int)((byte)num77));
						float arg_5E30_5 = 0f;
						origin = default(Vector2);
						arg_5E30_0.Draw(arg_5E30_1, arg_5E30_2, arg_5E30_3, arg_5E30_4, arg_5E30_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
						if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type > 0 && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].stack > 0)
						{
							float num79 = 1f;
							if (Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width > 32 || Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height > 32)
							{
								if (Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width > Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height)
								{
									num79 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width;
								}
								else
								{
									num79 = 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height;
								}
							}
							num79 *= Main.inventoryScale;
							SpriteBatch arg_60F3_0 = this.spriteBatch;
							Texture2D arg_60F3_1 = Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type];
							Vector2 arg_60F3_2 = new Vector2((float)num75 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width * 0.5f * num79, (float)num76 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height * 0.5f * num79);
							Rectangle? arg_60F3_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height));
							Color arg_60F3_4 = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].GetAlpha(white7);
							float arg_60F3_5 = 0f;
							origin = default(Vector2);
							arg_60F3_0.Draw(arg_60F3_1, arg_60F3_2, arg_60F3_3, arg_60F3_4, arg_60F3_5, origin, num79, SpriteEffects.None, 0f);
							Color arg_6124_0 = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].color;
							Color b = default(Color);
							if (arg_6124_0 != b)
							{
								SpriteBatch arg_627C_0 = this.spriteBatch;
								Texture2D arg_627C_1 = Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type];
								Vector2 arg_627C_2 = new Vector2((float)num75 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width * 0.5f * num79, (float)num76 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height * 0.5f * num79);
								Rectangle? arg_627C_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].type].Height));
								Color arg_627C_4 = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].GetColor(white7);
								float arg_627C_5 = 0f;
								origin = default(Vector2);
								arg_627C_0.Draw(arg_627C_1, arg_627C_2, arg_627C_3, arg_627C_4, arg_627C_5, origin, num79, SpriteEffects.None, 0f);
							}
							if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].stack > 1)
							{
								SpriteBatch arg_6315_0 = this.spriteBatch;
								SpriteFont arg_6315_1 = Main.fontItemStack;
								string arg_6315_2 = string.Concat(Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num74].stack);
								Vector2 arg_6315_3 = new Vector2((float)num75 + 10f * Main.inventoryScale, (float)num76 + 26f * Main.inventoryScale);
								Color arg_6315_4 = white7;
								float arg_6315_5 = 0f;
								origin = default(Vector2);
								arg_6315_0.DrawString(arg_6315_1, arg_6315_2, arg_6315_3, arg_6315_4, arg_6315_5, origin, num79, SpriteEffects.None, 0f);
							}
						}
						num74++;
					}
				}
				SpriteBatch arg_6382_0 = this.spriteBatch;
				SpriteFont arg_6382_1 = Main.fontMouseText;
				string arg_6382_2 = "Coins";
				Vector2 arg_6382_3 = new Vector2(496f, 84f);
				Color arg_6382_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_6382_5 = 0f;
				origin = default(Vector2);
				arg_6382_0.DrawString(arg_6382_1, arg_6382_2, arg_6382_3, arg_6382_4, arg_6382_5, origin, 0.75f, SpriteEffects.None, 0f);
				Main.inventoryScale = 0.6f;
				for (int num80 = 0; num80 < 4; num80++)
				{
					int num81 = 497;
					int num82 = (int)(85f + (float)(num80 * 56) * Main.inventoryScale + 20f);
					int num83 = num80 + 40;
					Color white8 = new Color(100, 100, 100, 100);
					if (Main.mouseState.X >= num81 && (float)Main.mouseState.X <= (float)num81 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num82 && (float)Main.mouseState.Y <= (float)num82 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
						{
							if ((Main.player[Main.myPlayer].selectedItem != num83 || Main.player[Main.myPlayer].itemAnimation <= 0) && (Main.mouseItem.type == 0 || Main.mouseItem.type == 71 || Main.mouseItem.type == 72 || Main.mouseItem.type == 73 || Main.mouseItem.type == 74))
							{
								Item item6 = Main.mouseItem;
								Main.mouseItem = Main.player[Main.myPlayer].inventory[num83];
								Main.player[Main.myPlayer].inventory[num83] = item6;
								if (Main.player[Main.myPlayer].inventory[num83].type == 0 || Main.player[Main.myPlayer].inventory[num83].stack < 1)
								{
									Main.player[Main.myPlayer].inventory[num83] = new Item();
								}
								if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num83]) && Main.player[Main.myPlayer].inventory[num83].stack != Main.player[Main.myPlayer].inventory[num83].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
								{
									if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[num83].stack <= Main.mouseItem.maxStack)
									{
										Main.player[Main.myPlayer].inventory[num83].stack += Main.mouseItem.stack;
										Main.mouseItem.stack = 0;
									}
									else
									{
										int num84 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[num83].stack;
										Main.player[Main.myPlayer].inventory[num83].stack += num84;
										Main.mouseItem.stack -= num84;
									}
								}
								if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
								{
									Main.mouseItem = new Item();
								}
								if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[num83].type > 0)
								{
									Main.PlaySound(7, -1, -1, 1);
								}
								Recipe.FindRecipes();
							}
						}
						else
						{
							if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[num83]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
							{
								if (Main.mouseItem.type == 0)
								{
									Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[num83].Clone();
									Main.mouseItem.stack = 0;
								}
								Main.mouseItem.stack++;
								Main.player[Main.myPlayer].inventory[num83].stack--;
								if (Main.player[Main.myPlayer].inventory[num83].stack <= 0)
								{
									Main.player[Main.myPlayer].inventory[num83] = new Item();
								}
								Recipe.FindRecipes();
								Main.soundInstanceMenuTick.Stop();
								Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
								Main.PlaySound(12, -1, -1, 1);
								if (Main.stackSplit == 0)
								{
									Main.stackSplit = 15;
								}
								else
								{
									Main.stackSplit = Main.stackDelay;
								}
							}
						}
						text5 = Main.player[Main.myPlayer].inventory[num83].name;
						Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[num83].Clone();
						if (Main.player[Main.myPlayer].inventory[num83].stack > 1)
						{
							object obj = text5;
							text5 = string.Concat(new object[]
							{
								obj, 
								" (", 
								Main.player[Main.myPlayer].inventory[num83].stack, 
								")"
							});
						}
					}
					SpriteBatch arg_693F_0 = this.spriteBatch;
					Texture2D arg_693F_1 = Main.inventoryBackTexture;
					Vector2 arg_693F_2 = new Vector2((float)num81, (float)num82);
					Rectangle? arg_693F_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
					Color arg_693F_4 = color2;
					float arg_693F_5 = 0f;
					origin = default(Vector2);
					arg_693F_0.Draw(arg_693F_1, arg_693F_2, arg_693F_3, arg_693F_4, arg_693F_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
					white8 = Color.White;
					if (Main.player[Main.myPlayer].inventory[num83].type > 0 && Main.player[Main.myPlayer].inventory[num83].stack > 0)
					{
						float num85 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height)
							{
								num85 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width;
							}
							else
							{
								num85 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height;
							}
						}
						num85 *= Main.inventoryScale;
						SpriteBatch arg_6BB5_0 = this.spriteBatch;
						Texture2D arg_6BB5_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type];
						Vector2 arg_6BB5_2 = new Vector2((float)num81 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width * 0.5f * num85, (float)num82 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height * 0.5f * num85);
						Rectangle? arg_6BB5_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height));
						Color arg_6BB5_4 = Main.player[Main.myPlayer].inventory[num83].GetAlpha(white8);
						float arg_6BB5_5 = 0f;
						origin = default(Vector2);
						arg_6BB5_0.Draw(arg_6BB5_1, arg_6BB5_2, arg_6BB5_3, arg_6BB5_4, arg_6BB5_5, origin, num85, SpriteEffects.None, 0f);
						Color arg_6BE0_0 = Main.player[Main.myPlayer].inventory[num83].color;
						Color b = default(Color);
						if (arg_6BE0_0 != b)
						{
							SpriteBatch arg_6D14_0 = this.spriteBatch;
							Texture2D arg_6D14_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type];
							Vector2 arg_6D14_2 = new Vector2((float)num81 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width * 0.5f * num85, (float)num82 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height * 0.5f * num85);
							Rectangle? arg_6D14_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num83].type].Height));
							Color arg_6D14_4 = Main.player[Main.myPlayer].inventory[num83].GetColor(white8);
							float arg_6D14_5 = 0f;
							origin = default(Vector2);
							arg_6D14_0.Draw(arg_6D14_1, arg_6D14_2, arg_6D14_3, arg_6D14_4, arg_6D14_5, origin, num85, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num83].stack > 1)
						{
							SpriteBatch arg_6DA1_0 = this.spriteBatch;
							SpriteFont arg_6DA1_1 = Main.fontItemStack;
							string arg_6DA1_2 = string.Concat(Main.player[Main.myPlayer].inventory[num83].stack);
							Vector2 arg_6DA1_3 = new Vector2((float)num81 + 10f * Main.inventoryScale, (float)num82 + 26f * Main.inventoryScale);
							Color arg_6DA1_4 = white8;
							float arg_6DA1_5 = 0f;
							origin = default(Vector2);
							arg_6DA1_0.DrawString(arg_6DA1_1, arg_6DA1_2, arg_6DA1_3, arg_6DA1_4, arg_6DA1_5, origin, num85, SpriteEffects.None, 0f);
						}
					}
				}
				SpriteBatch arg_6E0A_0 = this.spriteBatch;
				SpriteFont arg_6E0A_1 = Main.fontMouseText;
				string arg_6E0A_2 = "Ammo";
				Vector2 arg_6E0A_3 = new Vector2(532f, 84f);
				Color arg_6E0A_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_6E0A_5 = 0f;
				origin = default(Vector2);
				arg_6E0A_0.DrawString(arg_6E0A_1, arg_6E0A_2, arg_6E0A_3, arg_6E0A_4, arg_6E0A_5, origin, 0.75f, SpriteEffects.None, 0f);
				Main.inventoryScale = 0.6f;
				for (int num86 = 0; num86 < 4; num86++)
				{
					int num87 = 534;
					int num88 = (int)(85f + (float)(num86 * 56) * Main.inventoryScale + 20f);
					int num89 = num86;
					Color white9 = new Color(100, 100, 100, 100);
					if (Main.mouseState.X >= num87 && (float)Main.mouseState.X <= (float)num87 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num88 && (float)Main.mouseState.Y <= (float)num88 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
						{
							if ((Main.player[Main.myPlayer].selectedItem != num89 || Main.player[Main.myPlayer].itemAnimation <= 0) && (Main.mouseItem.type == 0 || Main.mouseItem.ammo > 0))
							{
								Item item7 = Main.mouseItem;
								Main.mouseItem = Main.player[Main.myPlayer].ammo[num89];
								Main.player[Main.myPlayer].ammo[num89] = item7;
								if (Main.player[Main.myPlayer].ammo[num89].type == 0 || Main.player[Main.myPlayer].ammo[num89].stack < 1)
								{
									Main.player[Main.myPlayer].ammo[num89] = new Item();
								}
								if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].ammo[num89]) && Main.player[Main.myPlayer].ammo[num89].stack != Main.player[Main.myPlayer].ammo[num89].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
								{
									if (Main.mouseItem.stack + Main.player[Main.myPlayer].ammo[num89].stack <= Main.mouseItem.maxStack)
									{
										Main.player[Main.myPlayer].ammo[num89].stack += Main.mouseItem.stack;
										Main.mouseItem.stack = 0;
									}
									else
									{
										int num90 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].ammo[num89].stack;
										Main.player[Main.myPlayer].ammo[num89].stack += num90;
										Main.mouseItem.stack -= num90;
									}
								}
								if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
								{
									Main.mouseItem = new Item();
								}
								if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].ammo[num89].type > 0)
								{
									Main.PlaySound(7, -1, -1, 1);
								}
								Recipe.FindRecipes();
							}
						}
						else
						{
							if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].ammo[num89]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
							{
								if (Main.mouseItem.type == 0)
								{
									Main.mouseItem = (Item)Main.player[Main.myPlayer].ammo[num89].Clone();
									Main.mouseItem.stack = 0;
								}
								Main.mouseItem.stack++;
								Main.player[Main.myPlayer].ammo[num89].stack--;
								if (Main.player[Main.myPlayer].ammo[num89].stack <= 0)
								{
									Main.player[Main.myPlayer].ammo[num89] = new Item();
								}
								Recipe.FindRecipes();
								Main.soundInstanceMenuTick.Stop();
								Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
								Main.PlaySound(12, -1, -1, 1);
								if (Main.stackSplit == 0)
								{
									Main.stackSplit = 15;
								}
								else
								{
									Main.stackSplit = Main.stackDelay;
								}
							}
						}
						text5 = Main.player[Main.myPlayer].ammo[num89].name;
						Main.toolTip = (Item)Main.player[Main.myPlayer].ammo[num89].Clone();
						if (Main.player[Main.myPlayer].ammo[num89].stack > 1)
						{
							object obj = text5;
							text5 = string.Concat(new object[]
							{
								obj, 
								" (", 
								Main.player[Main.myPlayer].ammo[num89].stack, 
								")"
							});
						}
					}
					SpriteBatch arg_7399_0 = this.spriteBatch;
					Texture2D arg_7399_1 = Main.inventoryBackTexture;
					Vector2 arg_7399_2 = new Vector2((float)num87, (float)num88);
					Rectangle? arg_7399_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
					Color arg_7399_4 = color2;
					float arg_7399_5 = 0f;
					origin = default(Vector2);
					arg_7399_0.Draw(arg_7399_1, arg_7399_2, arg_7399_3, arg_7399_4, arg_7399_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
					white9 = Color.White;
					if (Main.player[Main.myPlayer].ammo[num89].type > 0 && Main.player[Main.myPlayer].ammo[num89].stack > 0)
					{
						float num91 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width > Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height)
							{
								num91 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width;
							}
							else
							{
								num91 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height;
							}
						}
						num91 *= Main.inventoryScale;
						SpriteBatch arg_760F_0 = this.spriteBatch;
						Texture2D arg_760F_1 = Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type];
						Vector2 arg_760F_2 = new Vector2((float)num87 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width * 0.5f * num91, (float)num88 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height * 0.5f * num91);
						Rectangle? arg_760F_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width, Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height));
						Color arg_760F_4 = Main.player[Main.myPlayer].ammo[num89].GetAlpha(white9);
						float arg_760F_5 = 0f;
						origin = default(Vector2);
						arg_760F_0.Draw(arg_760F_1, arg_760F_2, arg_760F_3, arg_760F_4, arg_760F_5, origin, num91, SpriteEffects.None, 0f);
						Color arg_763A_0 = Main.player[Main.myPlayer].ammo[num89].color;
						Color b = default(Color);
						if (arg_763A_0 != b)
						{
							SpriteBatch arg_776E_0 = this.spriteBatch;
							Texture2D arg_776E_1 = Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type];
							Vector2 arg_776E_2 = new Vector2((float)num87 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width * 0.5f * num91, (float)num88 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height * 0.5f * num91);
							Rectangle? arg_776E_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Width, Main.itemTexture[Main.player[Main.myPlayer].ammo[num89].type].Height));
							Color arg_776E_4 = Main.player[Main.myPlayer].ammo[num89].GetColor(white9);
							float arg_776E_5 = 0f;
							origin = default(Vector2);
							arg_776E_0.Draw(arg_776E_1, arg_776E_2, arg_776E_3, arg_776E_4, arg_776E_5, origin, num91, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].ammo[num89].stack > 1)
						{
							SpriteBatch arg_77FB_0 = this.spriteBatch;
							SpriteFont arg_77FB_1 = Main.fontItemStack;
							string arg_77FB_2 = string.Concat(Main.player[Main.myPlayer].ammo[num89].stack);
							Vector2 arg_77FB_3 = new Vector2((float)num87 + 10f * Main.inventoryScale, (float)num88 + 26f * Main.inventoryScale);
							Color arg_77FB_4 = white9;
							float arg_77FB_5 = 0f;
							origin = default(Vector2);
							arg_77FB_0.DrawString(arg_77FB_1, arg_77FB_2, arg_77FB_3, arg_77FB_4, arg_77FB_5, origin, num91, SpriteEffects.None, 0f);
						}
					}
				}
				if (Main.npcShop > 0 && (!Main.playerInventory || Main.player[Main.myPlayer].talkNPC == -1))
				{
					Main.npcShop = 0;
				}
				if (Main.npcShop > 0)
				{
					SpriteBatch arg_7897_0 = this.spriteBatch;
					SpriteFont arg_7897_1 = Main.fontMouseText;
					string arg_7897_2 = "Shop";
					Vector2 arg_7897_3 = new Vector2(284f, 210f);
					Color arg_7897_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					float arg_7897_5 = 0f;
					origin = default(Vector2);
					arg_7897_0.DrawString(arg_7897_1, arg_7897_2, arg_7897_3, arg_7897_4, arg_7897_5, origin, 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseState.X > 73 && Main.mouseState.X < (int)(73f + 280f * Main.inventoryScale) && Main.mouseState.Y > 210 && Main.mouseState.Y < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num92 = 0; num92 < 5; num92++)
					{
						for (int num93 = 0; num93 < 4; num93++)
						{
							int num94 = (int)(73f + (float)(num92 * 56) * Main.inventoryScale);
							int num95 = (int)(210f + (float)(num93 * 56) * Main.inventoryScale);
							int num96 = num92 + num93 * 5;
							Color white10 = new Color(100, 100, 100, 100);
							if (Main.mouseState.X >= num94 && (float)Main.mouseState.X <= (float)num94 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num95 && (float)Main.mouseState.Y <= (float)num95 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									if (Main.mouseItem.type == 0)
									{
										if ((Main.player[Main.myPlayer].selectedItem != num96 || Main.player[Main.myPlayer].itemAnimation <= 0) && Main.player[Main.myPlayer].BuyItem(this.shop[Main.npcShop].item[num96].value))
										{
											Main.mouseItem.SetDefaults(this.shop[Main.npcShop].item[num96].name);
											if (this.shop[Main.npcShop].item[num96].buyOnce)
											{
												this.shop[Main.npcShop].item[num96].stack--;
												if (this.shop[Main.npcShop].item[num96].stack <= 0)
												{
													this.shop[Main.npcShop].item[num96].SetDefaults(0, false);
												}
											}
											Main.PlaySound(18, -1, -1, 1);
										}
									}
									else
									{
										if (this.shop[Main.npcShop].item[num96].type == 0)
										{
											if (Main.player[Main.myPlayer].SellItem(Main.mouseItem.value, Main.mouseItem.stack))
											{
												this.shop[Main.npcShop].AddShop(Main.mouseItem);
												Main.mouseItem.stack = 0;
												Main.mouseItem.type = 0;
												Main.PlaySound(18, -1, -1, 1);
											}
											else
											{
												if (Main.mouseItem.value == 0)
												{
													this.shop[Main.npcShop].AddShop(Main.mouseItem);
													Main.mouseItem.stack = 0;
													Main.mouseItem.type = 0;
													Main.PlaySound(7, -1, -1, 1);
												}
											}
										}
									}
								}
								else
								{
									if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(this.shop[Main.npcShop].item[num96]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0) && Main.player[Main.myPlayer].BuyItem(this.shop[Main.npcShop].item[num96].value))
									{
										Main.PlaySound(18, -1, -1, 1);
										if (Main.mouseItem.type == 0)
										{
											Main.mouseItem.SetDefaults(this.shop[Main.npcShop].item[num96].name);
											Main.mouseItem.stack = 0;
										}
										Main.mouseItem.stack++;
										if (Main.stackSplit == 0)
										{
											Main.stackSplit = 15;
										}
										else
										{
											Main.stackSplit = Main.stackDelay;
										}
										if (this.shop[Main.npcShop].item[num96].buyOnce)
										{
											this.shop[Main.npcShop].item[num96].stack--;
											if (this.shop[Main.npcShop].item[num96].stack <= 0)
											{
												this.shop[Main.npcShop].item[num96].SetDefaults(0, false);
											}
										}
									}
								}
								text5 = this.shop[Main.npcShop].item[num96].name;
								Main.toolTip = (Item)this.shop[Main.npcShop].item[num96].Clone();
								Main.toolTip.buy = true;
								if (this.shop[Main.npcShop].item[num96].stack > 1)
								{
									object obj = text5;
									text5 = string.Concat(new object[]
									{
										obj, 
										" (", 
										this.shop[Main.npcShop].item[num96].stack, 
										")"
									});
								}
							}
							SpriteBatch arg_7E69_0 = this.spriteBatch;
							Texture2D arg_7E69_1 = Main.inventoryBack6Texture;
							Vector2 arg_7E69_2 = new Vector2((float)num94, (float)num95);
							Rectangle? arg_7E69_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_7E69_4 = color2;
							float arg_7E69_5 = 0f;
							origin = default(Vector2);
							arg_7E69_0.Draw(arg_7E69_1, arg_7E69_2, arg_7E69_3, arg_7E69_4, arg_7E69_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
							white10 = Color.White;
							if (this.shop[Main.npcShop].item[num96].type > 0 && this.shop[Main.npcShop].item[num96].stack > 0)
							{
								float num97 = 1f;
								if (Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width > 32 || Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height > 32)
								{
									if (Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width > Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height)
									{
										num97 = 32f / (float)Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width;
									}
									else
									{
										num97 = 32f / (float)Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height;
									}
								}
								num97 *= Main.inventoryScale;
								SpriteBatch arg_80ED_0 = this.spriteBatch;
								Texture2D arg_80ED_1 = Main.itemTexture[this.shop[Main.npcShop].item[num96].type];
								Vector2 arg_80ED_2 = new Vector2((float)num94 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width * 0.5f * num97, (float)num95 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height * 0.5f * num97);
								Rectangle? arg_80ED_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width, Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height));
								Color arg_80ED_4 = this.shop[Main.npcShop].item[num96].GetAlpha(white10);
								float arg_80ED_5 = 0f;
								origin = default(Vector2);
								arg_80ED_0.Draw(arg_80ED_1, arg_80ED_2, arg_80ED_3, arg_80ED_4, arg_80ED_5, origin, num97, SpriteEffects.None, 0f);
								Color arg_8119_0 = this.shop[Main.npcShop].item[num96].color;
								Color b = default(Color);
								if (arg_8119_0 != b)
								{
									SpriteBatch arg_8253_0 = this.spriteBatch;
									Texture2D arg_8253_1 = Main.itemTexture[this.shop[Main.npcShop].item[num96].type];
									Vector2 arg_8253_2 = new Vector2((float)num94 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width * 0.5f * num97, (float)num95 + 26f * Main.inventoryScale - (float)Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height * 0.5f * num97);
									Rectangle? arg_8253_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Width, Main.itemTexture[this.shop[Main.npcShop].item[num96].type].Height));
									Color arg_8253_4 = this.shop[Main.npcShop].item[num96].GetColor(white10);
									float arg_8253_5 = 0f;
									origin = default(Vector2);
									arg_8253_0.Draw(arg_8253_1, arg_8253_2, arg_8253_3, arg_8253_4, arg_8253_5, origin, num97, SpriteEffects.None, 0f);
								}
								if (this.shop[Main.npcShop].item[num96].stack > 1)
								{
									SpriteBatch arg_82E2_0 = this.spriteBatch;
									SpriteFont arg_82E2_1 = Main.fontItemStack;
									string arg_82E2_2 = string.Concat(this.shop[Main.npcShop].item[num96].stack);
									Vector2 arg_82E2_3 = new Vector2((float)num94 + 10f * Main.inventoryScale, (float)num95 + 26f * Main.inventoryScale);
									Color arg_82E2_4 = white10;
									float arg_82E2_5 = 0f;
									origin = default(Vector2);
									arg_82E2_0.DrawString(arg_82E2_1, arg_82E2_2, arg_82E2_3, arg_82E2_4, arg_82E2_5, origin, num97, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.player[Main.myPlayer].chest > -1 && Main.tile[Main.player[Main.myPlayer].chestX, Main.player[Main.myPlayer].chestY].type != 21)
				{
					Main.player[Main.myPlayer].chest = -1;
				}
				if (Main.player[Main.myPlayer].chest != -1)
				{
					Main.inventoryScale = 0.75f;
					if (Main.mouseState.X > 73 && Main.mouseState.X < (int)(73f + 280f * Main.inventoryScale) && Main.mouseState.Y > 210 && Main.mouseState.Y < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num98 = 0; num98 < 3; num98++)
					{
						int num99 = 286;
						int num100 = 250;
						float num101 = this.chestLootScale;
						string text10 = "Loot All";
						if (num98 == 1)
						{
							num100 += 26;
							num101 = this.chestDepositScale;
							text10 = "Deposit All";
						}
						else
						{
							if (num98 == 2)
							{
								num100 += 52;
								num101 = this.chestStackScale;
								text10 = "Quick Stack";
							}
						}
						Vector2 vector7 = Main.fontMouseText.MeasureString(text10) / 2f;
						Color color7 = new Color((int)((byte)((float)Main.mouseTextColor * num101)), (int)((byte)((float)Main.mouseTextColor * num101)), (int)((byte)((float)Main.mouseTextColor * num101)), (int)((byte)((float)Main.mouseTextColor * num101)));
						num99 += (int)(vector7.X * num101);
						this.spriteBatch.DrawString(Main.fontMouseText, text10, new Vector2((float)num99, (float)num100), color7, 0f, vector7, num101, SpriteEffects.None, 0f);
						vector7 *= num101;
						if ((float)Main.mouseState.X > (float)num99 - vector7.X && (float)Main.mouseState.X < (float)num99 + vector7.X && (float)Main.mouseState.Y > (float)num100 - vector7.Y && (float)Main.mouseState.Y < (float)num100 + vector7.Y)
						{
							if (num98 == 0)
							{
								if (!this.chestLootHover)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								this.chestLootHover = true;
							}
							else
							{
								if (num98 == 1)
								{
									if (!this.chestDepositHover)
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.chestDepositHover = true;
								}
								else
								{
									if (!this.chestStackHover)
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.chestStackHover = true;
								}
							}
							Main.player[Main.myPlayer].mouseInterface = true;
							num101 += 0.05f;
							if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease)
							{
								if (num98 == 0)
								{
									if (Main.player[Main.myPlayer].chest > -1)
									{
										for (int num102 = 0; num102 < 20; num102++)
										{
											if (Main.chest[Main.player[Main.myPlayer].chest].item[num102].type > 0)
											{
												Main.chest[Main.player[Main.myPlayer].chest].item[num102] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, Main.chest[Main.player[Main.myPlayer].chest].item[num102]);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num102, 0f, 0f, 0);
												}
											}
										}
									}
									else
									{
										if (Main.player[Main.myPlayer].chest == -3)
										{
											for (int num103 = 0; num103 < 20; num103++)
											{
												if (Main.player[Main.myPlayer].bank2[num103].type > 0)
												{
													Main.player[Main.myPlayer].bank2[num103] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, Main.player[Main.myPlayer].bank2[num103]);
												}
											}
										}
										else
										{
											for (int num104 = 0; num104 < 20; num104++)
											{
												if (Main.player[Main.myPlayer].bank[num104].type > 0)
												{
													Main.player[Main.myPlayer].bank[num104] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, Main.player[Main.myPlayer].bank[num104]);
												}
											}
										}
									}
								}
								else
								{
									if (num98 == 1)
									{
										for (int num105 = 40; num105 >= 10; num105--)
										{
											if (Main.player[Main.myPlayer].inventory[num105].stack > 0 && Main.player[Main.myPlayer].inventory[num105].type > 0)
											{
												if (Main.player[Main.myPlayer].inventory[num105].maxStack > 1)
												{
													for (int num106 = 0; num106 < 20; num106++)
													{
														if (Main.player[Main.myPlayer].chest > -1)
														{
															if (Main.chest[Main.player[Main.myPlayer].chest].item[num106].stack < Main.chest[Main.player[Main.myPlayer].chest].item[num106].maxStack && Main.player[Main.myPlayer].inventory[num105].IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[num106]))
															{
																int num107 = Main.player[Main.myPlayer].inventory[num105].stack;
																if (Main.player[Main.myPlayer].inventory[num105].stack + Main.chest[Main.player[Main.myPlayer].chest].item[num106].stack > Main.chest[Main.player[Main.myPlayer].chest].item[num106].maxStack)
																{
																	num107 = Main.chest[Main.player[Main.myPlayer].chest].item[num106].maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[num106].stack;
																}
																Main.player[Main.myPlayer].inventory[num105].stack -= num107;
																Main.chest[Main.player[Main.myPlayer].chest].item[num106].stack += num107;
																Main.ChestCoins();
																Main.PlaySound(7, -1, -1, 1);
																if (Main.player[Main.myPlayer].inventory[num105].stack <= 0)
																{
																	Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num106, 0f, 0f, 0);
																		break;
																	}
																	break;
																}
																else
																{
																	if (Main.chest[Main.player[Main.myPlayer].chest].item[num106].type == 0)
																	{
																		Main.chest[Main.player[Main.myPlayer].chest].item[num106] = (Item)Main.player[Main.myPlayer].inventory[num105].Clone();
																		Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																	}
																	if (Main.netMode == 1)
																	{
																		NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num106, 0f, 0f, 0);
																	}
																}
															}
														}
														else
														{
															if (Main.player[Main.myPlayer].chest == -3)
															{
																if (Main.player[Main.myPlayer].bank2[num106].stack < Main.player[Main.myPlayer].bank2[num106].maxStack && Main.player[Main.myPlayer].inventory[num105].IsTheSameAs(Main.player[Main.myPlayer].bank2[num106]))
																{
																	int num108 = Main.player[Main.myPlayer].inventory[num105].stack;
																	if (Main.player[Main.myPlayer].inventory[num105].stack + Main.player[Main.myPlayer].bank2[num106].stack > Main.player[Main.myPlayer].bank2[num106].maxStack)
																	{
																		num108 = Main.player[Main.myPlayer].bank2[num106].maxStack - Main.player[Main.myPlayer].bank2[num106].stack;
																	}
																	Main.player[Main.myPlayer].inventory[num105].stack -= num108;
																	Main.player[Main.myPlayer].bank2[num106].stack += num108;
																	Main.PlaySound(7, -1, -1, 1);
																	Main.BankCoins();
																	if (Main.player[Main.myPlayer].inventory[num105].stack <= 0)
																	{
																		Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																		break;
																	}
																	if (Main.player[Main.myPlayer].bank2[num106].type == 0)
																	{
																		Main.player[Main.myPlayer].bank2[num106] = (Item)Main.player[Main.myPlayer].inventory[num105].Clone();
																		Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																	}
																}
															}
															else
															{
																if (Main.player[Main.myPlayer].bank[num106].stack < Main.player[Main.myPlayer].bank[num106].maxStack && Main.player[Main.myPlayer].inventory[num105].IsTheSameAs(Main.player[Main.myPlayer].bank[num106]))
																{
																	int num109 = Main.player[Main.myPlayer].inventory[num105].stack;
																	if (Main.player[Main.myPlayer].inventory[num105].stack + Main.player[Main.myPlayer].bank[num106].stack > Main.player[Main.myPlayer].bank[num106].maxStack)
																	{
																		num109 = Main.player[Main.myPlayer].bank[num106].maxStack - Main.player[Main.myPlayer].bank[num106].stack;
																	}
																	Main.player[Main.myPlayer].inventory[num105].stack -= num109;
																	Main.player[Main.myPlayer].bank[num106].stack += num109;
																	Main.PlaySound(7, -1, -1, 1);
																	Main.BankCoins();
																	if (Main.player[Main.myPlayer].inventory[num105].stack <= 0)
																	{
																		Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																		break;
																	}
																	if (Main.player[Main.myPlayer].bank[num106].type == 0)
																	{
																		Main.player[Main.myPlayer].bank[num106] = (Item)Main.player[Main.myPlayer].inventory[num105].Clone();
																		Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																	}
																}
															}
														}
													}
												}
												if (Main.player[Main.myPlayer].inventory[num105].stack > 0)
												{
													if (Main.player[Main.myPlayer].chest > -1)
													{
														int num110 = 0;
														while (num110 < 20)
														{
															if (Main.chest[Main.player[Main.myPlayer].chest].item[num110].stack == 0)
															{
																Main.PlaySound(7, -1, -1, 1);
																Main.chest[Main.player[Main.myPlayer].chest].item[num110] = (Item)Main.player[Main.myPlayer].inventory[num105].Clone();
																Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																if (Main.netMode == 1)
																{
																	NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num110, 0f, 0f, 0);
																	break;
																}
																break;
															}
															else
															{
																num110++;
															}
														}
													}
													else
													{
														if (Main.player[Main.myPlayer].chest == -3)
														{
															for (int num111 = 0; num111 < 20; num111++)
															{
																if (Main.player[Main.myPlayer].bank2[num111].stack == 0)
																{
																	Main.PlaySound(7, -1, -1, 1);
																	Main.player[Main.myPlayer].bank2[num111] = (Item)Main.player[Main.myPlayer].inventory[num105].Clone();
																	Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																	break;
																}
															}
														}
														else
														{
															for (int num112 = 0; num112 < 20; num112++)
															{
																if (Main.player[Main.myPlayer].bank[num112].stack == 0)
																{
																	Main.PlaySound(7, -1, -1, 1);
																	Main.player[Main.myPlayer].bank[num112] = (Item)Main.player[Main.myPlayer].inventory[num105].Clone();
																	Main.player[Main.myPlayer].inventory[num105].SetDefaults(0, false);
																	break;
																}
															}
														}
													}
												}
											}
										}
									}
									else
									{
										if (Main.player[Main.myPlayer].chest > -1)
										{
											for (int num113 = 0; num113 < 20; num113++)
											{
												if (Main.chest[Main.player[Main.myPlayer].chest].item[num113].type > 0 && Main.chest[Main.player[Main.myPlayer].chest].item[num113].stack < Main.chest[Main.player[Main.myPlayer].chest].item[num113].maxStack)
												{
													for (int num114 = 0; num114 < 44; num114++)
													{
														if (Main.chest[Main.player[Main.myPlayer].chest].item[num113].IsTheSameAs(Main.player[Main.myPlayer].inventory[num114]))
														{
															int num115 = Main.player[Main.myPlayer].inventory[num114].stack;
															if (Main.chest[Main.player[Main.myPlayer].chest].item[num113].stack + num115 > Main.chest[Main.player[Main.myPlayer].chest].item[num113].maxStack)
															{
																num115 = Main.chest[Main.player[Main.myPlayer].chest].item[num113].maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[num113].stack;
															}
															Main.PlaySound(7, -1, -1, 1);
															Main.chest[Main.player[Main.myPlayer].chest].item[num113].stack += num115;
															Main.player[Main.myPlayer].inventory[num114].stack -= num115;
															Main.ChestCoins();
															if (Main.player[Main.myPlayer].inventory[num114].stack == 0)
															{
																Main.player[Main.myPlayer].inventory[num114].SetDefaults(0, false);
															}
															else
															{
																if (Main.chest[Main.player[Main.myPlayer].chest].item[num113].type == 0)
																{
																	Main.chest[Main.player[Main.myPlayer].chest].item[num113] = (Item)Main.player[Main.myPlayer].inventory[num114].Clone();
																	Main.player[Main.myPlayer].inventory[num114].SetDefaults(0, false);
																}
															}
															if (Main.netMode == 1)
															{
																NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num113, 0f, 0f, 0);
															}
														}
													}
												}
											}
										}
										else
										{
											if (Main.player[Main.myPlayer].chest == -3)
											{
												for (int num116 = 0; num116 < 20; num116++)
												{
													if (Main.player[Main.myPlayer].bank2[num116].type > 0 && Main.player[Main.myPlayer].bank2[num116].stack < Main.player[Main.myPlayer].bank2[num116].maxStack)
													{
														for (int num117 = 0; num117 < 44; num117++)
														{
															if (Main.player[Main.myPlayer].bank2[num116].IsTheSameAs(Main.player[Main.myPlayer].inventory[num117]))
															{
																int num118 = Main.player[Main.myPlayer].inventory[num117].stack;
																if (Main.player[Main.myPlayer].bank2[num116].stack + num118 > Main.player[Main.myPlayer].bank2[num116].maxStack)
																{
																	num118 = Main.player[Main.myPlayer].bank2[num116].maxStack - Main.player[Main.myPlayer].bank2[num116].stack;
																}
																Main.PlaySound(7, -1, -1, 1);
																Main.player[Main.myPlayer].bank2[num116].stack += num118;
																Main.player[Main.myPlayer].inventory[num117].stack -= num118;
																Main.BankCoins();
																if (Main.player[Main.myPlayer].inventory[num117].stack == 0)
																{
																	Main.player[Main.myPlayer].inventory[num117].SetDefaults(0, false);
																}
																else
																{
																	if (Main.player[Main.myPlayer].bank2[num116].type == 0)
																	{
																		Main.player[Main.myPlayer].bank2[num116] = (Item)Main.player[Main.myPlayer].inventory[num117].Clone();
																		Main.player[Main.myPlayer].inventory[num117].SetDefaults(0, false);
																	}
																}
															}
														}
													}
												}
											}
											else
											{
												for (int num119 = 0; num119 < 20; num119++)
												{
													if (Main.player[Main.myPlayer].bank[num119].type > 0 && Main.player[Main.myPlayer].bank[num119].stack < Main.player[Main.myPlayer].bank[num119].maxStack)
													{
														for (int num120 = 0; num120 < 44; num120++)
														{
															if (Main.player[Main.myPlayer].bank[num119].IsTheSameAs(Main.player[Main.myPlayer].inventory[num120]))
															{
																int num121 = Main.player[Main.myPlayer].inventory[num120].stack;
																if (Main.player[Main.myPlayer].bank[num119].stack + num121 > Main.player[Main.myPlayer].bank[num119].maxStack)
																{
																	num121 = Main.player[Main.myPlayer].bank[num119].maxStack - Main.player[Main.myPlayer].bank[num119].stack;
																}
																Main.PlaySound(7, -1, -1, 1);
																Main.player[Main.myPlayer].bank[num119].stack += num121;
																Main.player[Main.myPlayer].inventory[num120].stack -= num121;
																Main.BankCoins();
																if (Main.player[Main.myPlayer].inventory[num120].stack == 0)
																{
																	Main.player[Main.myPlayer].inventory[num120].SetDefaults(0, false);
																}
																else
																{
																	if (Main.player[Main.myPlayer].bank[num119].type == 0)
																	{
																		Main.player[Main.myPlayer].bank[num119] = (Item)Main.player[Main.myPlayer].inventory[num120].Clone();
																		Main.player[Main.myPlayer].inventory[num120].SetDefaults(0, false);
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
								Recipe.FindRecipes();
							}
						}
						else
						{
							num101 -= 0.05f;
							if (num98 == 0)
							{
								this.chestLootHover = false;
							}
							else
							{
								if (num98 == 1)
								{
									this.chestDepositHover = false;
								}
								else
								{
									this.chestStackHover = false;
								}
							}
						}
						if ((double)num101 < 0.75)
						{
							num101 = 0.75f;
						}
						if (num101 > 1f)
						{
							num101 = 1f;
						}
						if (num98 == 0)
						{
							this.chestLootScale = num101;
						}
						else
						{
							if (num98 == 1)
							{
								this.chestDepositScale = num101;
							}
							else
							{
								this.chestStackScale = num101;
							}
						}
					}
				}
				else
				{
					this.chestLootScale = 0.75f;
					this.chestDepositScale = 0.75f;
					this.chestStackScale = 0.75f;
					this.chestLootHover = false;
					this.chestDepositHover = false;
					this.chestStackHover = false;
				}
				if (Main.player[Main.myPlayer].chest > -1)
				{
					SpriteBatch arg_99A4_0 = this.spriteBatch;
					SpriteFont arg_99A4_1 = Main.fontMouseText;
					string arg_99A4_2 = Main.chestText;
					Vector2 arg_99A4_3 = new Vector2(284f, 210f);
					Color arg_99A4_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					float arg_99A4_5 = 0f;
					origin = default(Vector2);
					arg_99A4_0.DrawString(arg_99A4_1, arg_99A4_2, arg_99A4_3, arg_99A4_4, arg_99A4_5, origin, 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseState.X > 73 && Main.mouseState.X < (int)(73f + 280f * Main.inventoryScale) && Main.mouseState.Y > 210 && Main.mouseState.Y < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num122 = 0; num122 < 5; num122++)
					{
						for (int num123 = 0; num123 < 4; num123++)
						{
							int num124 = (int)(73f + (float)(num122 * 56) * Main.inventoryScale);
							int num125 = (int)(210f + (float)(num123 * 56) * Main.inventoryScale);
							int num126 = num122 + num123 * 5;
							Color white11 = new Color(100, 100, 100, 100);
							if (Main.mouseState.X >= num124 && (float)Main.mouseState.X <= (float)num124 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num125 && (float)Main.mouseState.Y <= (float)num125 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									if (Main.player[Main.myPlayer].selectedItem != num126 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item8 = Main.mouseItem;
										Main.mouseItem = Main.chest[Main.player[Main.myPlayer].chest].item[num126];
										Main.chest[Main.player[Main.myPlayer].chest].item[num126] = item8;
										if (Main.chest[Main.player[Main.myPlayer].chest].item[num126].type == 0 || Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack < 1)
										{
											Main.chest[Main.player[Main.myPlayer].chest].item[num126] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[num126]) && Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack != Main.chest[Main.player[Main.myPlayer].chest].item[num126].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack <= Main.mouseItem.maxStack)
											{
												Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num127 = Main.mouseItem.maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack;
												Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack += num127;
												Main.mouseItem.stack -= num127;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.chest[Main.player[Main.myPlayer].chest].item[num126].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num126, 0f, 0f, 0);
										}
									}
								}
								else
								{
									if (Main.mouseState.RightButton == ButtonState.Pressed && Main.mouseRightRelease && Main.chest[Main.player[Main.myPlayer].chest].item[num126].maxStack == 1)
									{
										Main.chest[Main.player[Main.myPlayer].chest].item[num126] = Main.armorSwap(Main.chest[Main.player[Main.myPlayer].chest].item[num126]);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num126, 0f, 0f, 0);
										}
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && Main.chest[Main.player[Main.myPlayer].chest].item[num126].maxStack > 1 && (Main.mouseItem.IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[num126]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.chest[Main.player[Main.myPlayer].chest].item[num126].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack--;
											if (Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack <= 0)
											{
												Main.chest[Main.player[Main.myPlayer].chest].item[num126] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
											if (Main.netMode == 1)
											{
												NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)num126, 0f, 0f, 0);
											}
										}
									}
								}
								text5 = Main.chest[Main.player[Main.myPlayer].chest].item[num126].name;
								Main.toolTip = (Item)Main.chest[Main.player[Main.myPlayer].chest].item[num126].Clone();
								if (Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack > 1)
								{
									object obj = text5;
									text5 = string.Concat(new object[]
									{
										obj, 
										" (", 
										Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack, 
										")"
									});
								}
							}
							SpriteBatch arg_A1DB_0 = this.spriteBatch;
							Texture2D arg_A1DB_1 = Main.inventoryBack5Texture;
							Vector2 arg_A1DB_2 = new Vector2((float)num124, (float)num125);
							Rectangle? arg_A1DB_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_A1DB_4 = color2;
							float arg_A1DB_5 = 0f;
							origin = default(Vector2);
							arg_A1DB_0.Draw(arg_A1DB_1, arg_A1DB_2, arg_A1DB_3, arg_A1DB_4, arg_A1DB_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
							white11 = Color.White;
							if (Main.chest[Main.player[Main.myPlayer].chest].item[num126].type > 0 && Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack > 0)
							{
								float num128 = 1f;
								if (Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width > 32 || Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height > 32)
								{
									if (Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width > Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height)
									{
										num128 = 32f / (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width;
									}
									else
									{
										num128 = 32f / (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height;
									}
								}
								num128 *= Main.inventoryScale;
								SpriteBatch arg_A4EB_0 = this.spriteBatch;
								Texture2D arg_A4EB_1 = Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type];
								Vector2 arg_A4EB_2 = new Vector2((float)num124 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width * 0.5f * num128, (float)num125 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height * 0.5f * num128);
								Rectangle? arg_A4EB_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height));
								Color arg_A4EB_4 = Main.chest[Main.player[Main.myPlayer].chest].item[num126].GetAlpha(white11);
								float arg_A4EB_5 = 0f;
								origin = default(Vector2);
								arg_A4EB_0.Draw(arg_A4EB_1, arg_A4EB_2, arg_A4EB_3, arg_A4EB_4, arg_A4EB_5, origin, num128, SpriteEffects.None, 0f);
								Color arg_A521_0 = Main.chest[Main.player[Main.myPlayer].chest].item[num126].color;
								Color b = default(Color);
								if (arg_A521_0 != b)
								{
									SpriteBatch arg_A697_0 = this.spriteBatch;
									Texture2D arg_A697_1 = Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type];
									Vector2 arg_A697_2 = new Vector2((float)num124 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width * 0.5f * num128, (float)num125 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height * 0.5f * num128);
									Rectangle? arg_A697_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Width, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[num126].type].Height));
									Color arg_A697_4 = Main.chest[Main.player[Main.myPlayer].chest].item[num126].GetColor(white11);
									float arg_A697_5 = 0f;
									origin = default(Vector2);
									arg_A697_0.Draw(arg_A697_1, arg_A697_2, arg_A697_3, arg_A697_4, arg_A697_5, origin, num128, SpriteEffects.None, 0f);
								}
								if (Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack > 1)
								{
									SpriteBatch arg_A73A_0 = this.spriteBatch;
									SpriteFont arg_A73A_1 = Main.fontItemStack;
									string arg_A73A_2 = string.Concat(Main.chest[Main.player[Main.myPlayer].chest].item[num126].stack);
									Vector2 arg_A73A_3 = new Vector2((float)num124 + 10f * Main.inventoryScale, (float)num125 + 26f * Main.inventoryScale);
									Color arg_A73A_4 = white11;
									float arg_A73A_5 = 0f;
									origin = default(Vector2);
									arg_A73A_0.DrawString(arg_A73A_1, arg_A73A_2, arg_A73A_3, arg_A73A_4, arg_A73A_5, origin, num128, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.player[Main.myPlayer].chest == -2)
				{
					SpriteBatch arg_A7C8_0 = this.spriteBatch;
					SpriteFont arg_A7C8_1 = Main.fontMouseText;
					string arg_A7C8_2 = "Piggy Bank";
					Vector2 arg_A7C8_3 = new Vector2(284f, 210f);
					Color arg_A7C8_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					float arg_A7C8_5 = 0f;
					origin = default(Vector2);
					arg_A7C8_0.DrawString(arg_A7C8_1, arg_A7C8_2, arg_A7C8_3, arg_A7C8_4, arg_A7C8_5, origin, 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseState.X > 73 && Main.mouseState.X < (int)(73f + 280f * Main.inventoryScale) && Main.mouseState.Y > 210 && Main.mouseState.Y < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num129 = 0; num129 < 5; num129++)
					{
						for (int num130 = 0; num130 < 4; num130++)
						{
							int num131 = (int)(73f + (float)(num129 * 56) * Main.inventoryScale);
							int num132 = (int)(210f + (float)(num130 * 56) * Main.inventoryScale);
							int num133 = num129 + num130 * 5;
							Color white12 = new Color(100, 100, 100, 100);
							if (Main.mouseState.X >= num131 && (float)Main.mouseState.X <= (float)num131 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num132 && (float)Main.mouseState.Y <= (float)num132 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									if (Main.player[Main.myPlayer].selectedItem != num133 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item9 = Main.mouseItem;
										Main.mouseItem = Main.player[Main.myPlayer].bank[num133];
										Main.player[Main.myPlayer].bank[num133] = item9;
										if (Main.player[Main.myPlayer].bank[num133].type == 0 || Main.player[Main.myPlayer].bank[num133].stack < 1)
										{
											Main.player[Main.myPlayer].bank[num133] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank[num133]) && Main.player[Main.myPlayer].bank[num133].stack != Main.player[Main.myPlayer].bank[num133].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.player[Main.myPlayer].bank[num133].stack <= Main.mouseItem.maxStack)
											{
												Main.player[Main.myPlayer].bank[num133].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num134 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].bank[num133].stack;
												Main.player[Main.myPlayer].bank[num133].stack += num134;
												Main.mouseItem.stack -= num134;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].bank[num133].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
								}
								else
								{
									if (Main.mouseState.RightButton == ButtonState.Pressed && Main.mouseRightRelease && Main.player[Main.myPlayer].bank[num133].maxStack == 1)
									{
										Main.player[Main.myPlayer].bank[num133] = Main.armorSwap(Main.player[Main.myPlayer].bank[num133]);
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && Main.player[Main.myPlayer].bank[num133].maxStack > 1 && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank[num133]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.player[Main.myPlayer].bank[num133].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.player[Main.myPlayer].bank[num133].stack--;
											if (Main.player[Main.myPlayer].bank[num133].stack <= 0)
											{
												Main.player[Main.myPlayer].bank[num133] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
										}
									}
								}
								text5 = Main.player[Main.myPlayer].bank[num133].name;
								Main.toolTip = (Item)Main.player[Main.myPlayer].bank[num133].Clone();
								if (Main.player[Main.myPlayer].bank[num133].stack > 1)
								{
									object obj = text5;
									text5 = string.Concat(new object[]
									{
										obj, 
										" (", 
										Main.player[Main.myPlayer].bank[num133].stack, 
										")"
									});
								}
							}
							SpriteBatch arg_AE3C_0 = this.spriteBatch;
							Texture2D arg_AE3C_1 = Main.inventoryBack2Texture;
							Vector2 arg_AE3C_2 = new Vector2((float)num131, (float)num132);
							Rectangle? arg_AE3C_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_AE3C_4 = color2;
							float arg_AE3C_5 = 0f;
							origin = default(Vector2);
							arg_AE3C_0.Draw(arg_AE3C_1, arg_AE3C_2, arg_AE3C_3, arg_AE3C_4, arg_AE3C_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
							white12 = Color.White;
							if (Main.player[Main.myPlayer].bank[num133].type > 0 && Main.player[Main.myPlayer].bank[num133].stack > 0)
							{
								float num135 = 1f;
								if (Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height > 32)
								{
									if (Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width > Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height)
									{
										num135 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width;
									}
									else
									{
										num135 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height;
									}
								}
								num135 *= Main.inventoryScale;
								SpriteBatch arg_B0B2_0 = this.spriteBatch;
								Texture2D arg_B0B2_1 = Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type];
								Vector2 arg_B0B2_2 = new Vector2((float)num131 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width * 0.5f * num135, (float)num132 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height * 0.5f * num135);
								Rectangle? arg_B0B2_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height));
								Color arg_B0B2_4 = Main.player[Main.myPlayer].bank[num133].GetAlpha(white12);
								float arg_B0B2_5 = 0f;
								origin = default(Vector2);
								arg_B0B2_0.Draw(arg_B0B2_1, arg_B0B2_2, arg_B0B2_3, arg_B0B2_4, arg_B0B2_5, origin, num135, SpriteEffects.None, 0f);
								Color arg_B0DD_0 = Main.player[Main.myPlayer].bank[num133].color;
								Color b = default(Color);
								if (arg_B0DD_0 != b)
								{
									SpriteBatch arg_B211_0 = this.spriteBatch;
									Texture2D arg_B211_1 = Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type];
									Vector2 arg_B211_2 = new Vector2((float)num131 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width * 0.5f * num135, (float)num132 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height * 0.5f * num135);
									Rectangle? arg_B211_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank[num133].type].Height));
									Color arg_B211_4 = Main.player[Main.myPlayer].bank[num133].GetColor(white12);
									float arg_B211_5 = 0f;
									origin = default(Vector2);
									arg_B211_0.Draw(arg_B211_1, arg_B211_2, arg_B211_3, arg_B211_4, arg_B211_5, origin, num135, SpriteEffects.None, 0f);
								}
								if (Main.player[Main.myPlayer].bank[num133].stack > 1)
								{
									SpriteBatch arg_B29E_0 = this.spriteBatch;
									SpriteFont arg_B29E_1 = Main.fontItemStack;
									string arg_B29E_2 = string.Concat(Main.player[Main.myPlayer].bank[num133].stack);
									Vector2 arg_B29E_3 = new Vector2((float)num131 + 10f * Main.inventoryScale, (float)num132 + 26f * Main.inventoryScale);
									Color arg_B29E_4 = white12;
									float arg_B29E_5 = 0f;
									origin = default(Vector2);
									arg_B29E_0.DrawString(arg_B29E_1, arg_B29E_2, arg_B29E_3, arg_B29E_4, arg_B29E_5, origin, num135, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
				if (Main.player[Main.myPlayer].chest == -3)
				{
					SpriteBatch arg_B32C_0 = this.spriteBatch;
					SpriteFont arg_B32C_1 = Main.fontMouseText;
					string arg_B32C_2 = "Safe";
					Vector2 arg_B32C_3 = new Vector2(284f, 210f);
					Color arg_B32C_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					float arg_B32C_5 = 0f;
					origin = default(Vector2);
					arg_B32C_0.DrawString(arg_B32C_1, arg_B32C_2, arg_B32C_3, arg_B32C_4, arg_B32C_5, origin, 1f, SpriteEffects.None, 0f);
					Main.inventoryScale = 0.75f;
					if (Main.mouseState.X > 73 && Main.mouseState.X < (int)(73f + 280f * Main.inventoryScale) && Main.mouseState.Y > 210 && Main.mouseState.Y < (int)(210f + 224f * Main.inventoryScale))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					for (int num136 = 0; num136 < 5; num136++)
					{
						for (int num137 = 0; num137 < 4; num137++)
						{
							int num138 = (int)(73f + (float)(num136 * 56) * Main.inventoryScale);
							int num139 = (int)(210f + (float)(num137 * 56) * Main.inventoryScale);
							int num140 = num136 + num137 * 5;
							Color white13 = new Color(100, 100, 100, 100);
							if (Main.mouseState.X >= num138 && (float)Main.mouseState.X <= (float)num138 + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseState.Y >= num139 && (float)Main.mouseState.Y <= (float)num139 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									if (Main.player[Main.myPlayer].selectedItem != num140 || Main.player[Main.myPlayer].itemAnimation <= 0)
									{
										Item item10 = Main.mouseItem;
										Main.mouseItem = Main.player[Main.myPlayer].bank2[num140];
										Main.player[Main.myPlayer].bank2[num140] = item10;
										if (Main.player[Main.myPlayer].bank2[num140].type == 0 || Main.player[Main.myPlayer].bank2[num140].stack < 1)
										{
											Main.player[Main.myPlayer].bank2[num140] = new Item();
										}
										if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank2[num140]) && Main.player[Main.myPlayer].bank2[num140].stack != Main.player[Main.myPlayer].bank2[num140].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
										{
											if (Main.mouseItem.stack + Main.player[Main.myPlayer].bank2[num140].stack <= Main.mouseItem.maxStack)
											{
												Main.player[Main.myPlayer].bank2[num140].stack += Main.mouseItem.stack;
												Main.mouseItem.stack = 0;
											}
											else
											{
												int num141 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].bank2[num140].stack;
												Main.player[Main.myPlayer].bank2[num140].stack += num141;
												Main.mouseItem.stack -= num141;
											}
										}
										if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
										{
											Main.mouseItem = new Item();
										}
										if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].bank2[num140].type > 0)
										{
											Recipe.FindRecipes();
											Main.PlaySound(7, -1, -1, 1);
										}
									}
								}
								else
								{
									if (Main.mouseState.RightButton == ButtonState.Pressed && Main.mouseRightRelease && Main.player[Main.myPlayer].bank2[num140].maxStack == 1)
									{
										Main.player[Main.myPlayer].bank2[num140] = Main.armorSwap(Main.player[Main.myPlayer].bank2[num140]);
									}
									else
									{
										if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && Main.player[Main.myPlayer].bank2[num140].maxStack > 1 && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank2[num140]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
										{
											if (Main.mouseItem.type == 0)
											{
												Main.mouseItem = (Item)Main.player[Main.myPlayer].bank2[num140].Clone();
												Main.mouseItem.stack = 0;
											}
											Main.mouseItem.stack++;
											Main.player[Main.myPlayer].bank2[num140].stack--;
											if (Main.player[Main.myPlayer].bank2[num140].stack <= 0)
											{
												Main.player[Main.myPlayer].bank2[num140] = new Item();
											}
											Recipe.FindRecipes();
											Main.soundInstanceMenuTick.Stop();
											Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
											Main.PlaySound(12, -1, -1, 1);
											if (Main.stackSplit == 0)
											{
												Main.stackSplit = 15;
											}
											else
											{
												Main.stackSplit = Main.stackDelay;
											}
										}
									}
								}
								text5 = Main.player[Main.myPlayer].bank2[num140].name;
								Main.toolTip = (Item)Main.player[Main.myPlayer].bank2[num140].Clone();
								if (Main.player[Main.myPlayer].bank2[num140].stack > 1)
								{
									object obj = text5;
									text5 = string.Concat(new object[]
									{
										obj, 
										" (", 
										Main.player[Main.myPlayer].bank2[num140].stack, 
										")"
									});
								}
							}
							SpriteBatch arg_B9A0_0 = this.spriteBatch;
							Texture2D arg_B9A0_1 = Main.inventoryBack2Texture;
							Vector2 arg_B9A0_2 = new Vector2((float)num138, (float)num139);
							Rectangle? arg_B9A0_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
							Color arg_B9A0_4 = color2;
							float arg_B9A0_5 = 0f;
							origin = default(Vector2);
							arg_B9A0_0.Draw(arg_B9A0_1, arg_B9A0_2, arg_B9A0_3, arg_B9A0_4, arg_B9A0_5, origin, Main.inventoryScale, SpriteEffects.None, 0f);
							white13 = Color.White;
							if (Main.player[Main.myPlayer].bank2[num140].type > 0 && Main.player[Main.myPlayer].bank2[num140].stack > 0)
							{
								float num142 = 1f;
								if (Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height > 32)
								{
									if (Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width > Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height)
									{
										num142 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width;
									}
									else
									{
										num142 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height;
									}
								}
								num142 *= Main.inventoryScale;
								SpriteBatch arg_BC16_0 = this.spriteBatch;
								Texture2D arg_BC16_1 = Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type];
								Vector2 arg_BC16_2 = new Vector2((float)num138 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width * 0.5f * num142, (float)num139 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height * 0.5f * num142);
								Rectangle? arg_BC16_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height));
								Color arg_BC16_4 = Main.player[Main.myPlayer].bank2[num140].GetAlpha(white13);
								float arg_BC16_5 = 0f;
								origin = default(Vector2);
								arg_BC16_0.Draw(arg_BC16_1, arg_BC16_2, arg_BC16_3, arg_BC16_4, arg_BC16_5, origin, num142, SpriteEffects.None, 0f);
								Color arg_BC41_0 = Main.player[Main.myPlayer].bank2[num140].color;
								Color b = default(Color);
								if (arg_BC41_0 != b)
								{
									SpriteBatch arg_BD75_0 = this.spriteBatch;
									Texture2D arg_BD75_1 = Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type];
									Vector2 arg_BD75_2 = new Vector2((float)num138 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width * 0.5f * num142, (float)num139 + 26f * Main.inventoryScale - (float)Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height * 0.5f * num142);
									Rectangle? arg_BD75_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank2[num140].type].Height));
									Color arg_BD75_4 = Main.player[Main.myPlayer].bank2[num140].GetColor(white13);
									float arg_BD75_5 = 0f;
									origin = default(Vector2);
									arg_BD75_0.Draw(arg_BD75_1, arg_BD75_2, arg_BD75_3, arg_BD75_4, arg_BD75_5, origin, num142, SpriteEffects.None, 0f);
								}
								if (Main.player[Main.myPlayer].bank2[num140].stack > 1)
								{
									SpriteBatch arg_BE02_0 = this.spriteBatch;
									SpriteFont arg_BE02_1 = Main.fontItemStack;
									string arg_BE02_2 = string.Concat(Main.player[Main.myPlayer].bank2[num140].stack);
									Vector2 arg_BE02_3 = new Vector2((float)num138 + 10f * Main.inventoryScale, (float)num139 + 26f * Main.inventoryScale);
									Color arg_BE02_4 = white13;
									float arg_BE02_5 = 0f;
									origin = default(Vector2);
									arg_BE02_0.DrawString(arg_BE02_1, arg_BE02_2, arg_BE02_3, arg_BE02_4, arg_BE02_5, origin, num142, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag5 = false;
				bool flag6 = false;
				for (int num143 = 0; num143 < 3; num143++)
				{
					string text11 = "";
					if (Main.player[Main.myPlayer].accDepthMeter > 0 && !flag6)
					{
						int num144 = (int)((double)((Main.player[Main.myPlayer].position.Y + (float)Main.player[Main.myPlayer].height) * 2f / 16f) - Main.worldSurface * 2.0);
						if (num144 > 0)
						{
							text11 = "Depth: " + num144 + " feet below";
							if (num144 == 1)
							{
								text11 = "Depth: " + num144 + " foot below";
							}
						}
						else
						{
							if (num144 < 0)
							{
								num144 *= -1;
								text11 = "Depth: " + num144 + " feet above";
								if (num144 == 1)
								{
									text11 = "Depth: " + num144 + " foot above";
								}
							}
							else
							{
								text11 = "Depth: Level";
							}
						}
						flag6 = true;
					}
					else
					{
						if (Main.player[Main.myPlayer].accWatch > 0 && !flag5)
						{
							string text12 = "AM";
							double num145 = Main.time;
							if (!Main.dayTime)
							{
								num145 += 54000.0;
							}
							num145 = num145 / 86400.0 * 24.0;
							double num146 = 7.5;
							num145 = num145 - num146 - 12.0;
							if (num145 < 0.0)
							{
								num145 += 24.0;
							}
							if (num145 >= 12.0)
							{
								text12 = "PM";
							}
							int num147 = (int)num145;
							double num148 = num145 - (double)num147;
							num148 = (double)((int)(num148 * 60.0));
							string text13 = string.Concat(num148);
							if (num148 < 10.0)
							{
								text13 = "0" + text13;
							}
							if (num147 > 12)
							{
								num147 -= 12;
							}
							if (num147 == 0)
							{
								num147 = 12;
							}
							if (Main.player[Main.myPlayer].accWatch == 1)
							{
								text13 = "00";
							}
							else
							{
								if (Main.player[Main.myPlayer].accWatch == 2)
								{
									if (num148 < 30.0)
									{
										text13 = "00";
									}
									else
									{
										text13 = "30";
									}
								}
							}
							text11 = string.Concat(new object[]
							{
								"Time: ", 
								num147, 
								":", 
								text13, 
								" ", 
								text12
							});
							flag5 = true;
						}
					}
					if (text11 != "")
					{
						for (int num149 = 0; num149 < 5; num149++)
						{
							int num150 = 0;
							int num151 = 0;
							Color black = Color.Black;
							if (num149 == 0)
							{
								num150 = -2;
							}
							if (num149 == 1)
							{
								num150 = 2;
							}
							if (num149 == 2)
							{
								num151 = -2;
							}
							if (num149 == 3)
							{
								num151 = 2;
							}
							if (num149 == 4)
							{
								black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
							}
							SpriteBatch arg_C17C_0 = this.spriteBatch;
							SpriteFont arg_C17C_1 = Main.fontMouseText;
							string arg_C17C_2 = text11;
							Vector2 arg_C17C_3 = new Vector2((float)(22 + num150), (float)(74 + 22 * num143 + num151 + 48));
							Color arg_C17C_4 = black;
							float arg_C17C_5 = 0f;
							origin = default(Vector2);
							arg_C17C_0.DrawString(arg_C17C_1, arg_C17C_2, arg_C17C_3, arg_C17C_4, arg_C17C_5, origin, 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
			if (Main.playerInventory || Main.player[Main.myPlayer].ghost)
			{
				string text14 = "Save & Exit";
				if (Main.netMode != 0)
				{
					text14 = "Disconnect";
				}
				Vector2 vector8 = Main.fontDeathText.MeasureString(text14);
				int num152 = Main.screenWidth - 110;
				int num153 = Main.screenHeight - 20;
				if (Main.mouseExit)
				{
					if (Main.exitScale < 1f)
					{
						Main.exitScale += 0.02f;
					}
				}
				else
				{
					if ((double)Main.exitScale > 0.8)
					{
						Main.exitScale -= 0.02f;
					}
				}
				for (int num154 = 0; num154 < 5; num154++)
				{
					int num155 = 0;
					int num156 = 0;
					Color color8 = Color.Black;
					if (num154 == 0)
					{
						num155 = -2;
					}
					if (num154 == 1)
					{
						num155 = 2;
					}
					if (num154 == 2)
					{
						num156 = -2;
					}
					if (num154 == 3)
					{
						num156 = 2;
					}
					if (num154 == 4)
					{
						color8 = Color.White;
					}
					this.spriteBatch.DrawString(Main.fontDeathText, text14, new Vector2((float)(num152 + num155), (float)(num153 + num156)), color8, 0f, new Vector2(vector8.X / 2f, vector8.Y / 2f), Main.exitScale - 0.2f, SpriteEffects.None, 0f);
				}
				if ((float)Main.mouseState.X > (float)num152 - vector8.X / 2f && (float)Main.mouseState.X < (float)num152 + vector8.X / 2f && (float)Main.mouseState.Y > (float)num153 - vector8.Y / 2f && (float)Main.mouseState.Y < (float)num153 + vector8.Y / 2f - 10f)
				{
					if (!Main.mouseExit)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					Main.mouseExit = true;
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
					{
						Main.menuMode = 10;
						WorldGen.SaveAndQuit();
					}
				}
				else
				{
					Main.mouseExit = false;
				}
			}
			if (!Main.playerInventory && !Main.player[Main.myPlayer].ghost)
			{
				string text15 = "Items";
				if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name != "" && Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name != null)
				{
					text15 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name;
				}
				Vector2 vector9 = Main.fontMouseText.MeasureString(text15) / 2f;
				SpriteBatch arg_C4CF_0 = this.spriteBatch;
				SpriteFont arg_C4CF_1 = Main.fontMouseText;
				string arg_C4CF_2 = text15;
				Vector2 arg_C4CF_3 = new Vector2(236f - vector9.X, 0f);
				Color arg_C4CF_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
				float arg_C4CF_5 = 0f;
				origin = default(Vector2);
				arg_C4CF_0.DrawString(arg_C4CF_1, arg_C4CF_2, arg_C4CF_3, arg_C4CF_4, arg_C4CF_5, origin, 1f, SpriteEffects.None, 0f);
				int num157 = 20;
				float num158 = 1f;
				for (int num159 = 0; num159 < 10; num159++)
				{
					if (num159 == Main.player[Main.myPlayer].selectedItem)
					{
						if (Main.hotbarScale[num159] < 1f)
						{
							Main.hotbarScale[num159] += 0.05f;
						}
					}
					else
					{
						if ((double)Main.hotbarScale[num159] > 0.75)
						{
							Main.hotbarScale[num159] -= 0.05f;
						}
					}
					float num160 = Main.hotbarScale[num159];
					int num161 = (int)(20f + 22f * (1f - num160));
					int a3 = (int)(75f + 150f * num160);
					Color color9 = new Color(255, 255, 255, a3);
					SpriteBatch arg_C602_0 = this.spriteBatch;
					Texture2D arg_C602_1 = Main.inventoryBackTexture;
					Vector2 arg_C602_2 = new Vector2((float)num157, (float)num161);
					Rectangle? arg_C602_3 = new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height));
					Color arg_C602_4 = new Color(100, 100, 100, 100);
					float arg_C602_5 = 0f;
					origin = default(Vector2);
					arg_C602_0.Draw(arg_C602_1, arg_C602_2, arg_C602_3, arg_C602_4, arg_C602_5, origin, num160, SpriteEffects.None, 0f);
					if (Main.mouseState.X >= num157 && (float)Main.mouseState.X <= (float)num157 + (float)Main.inventoryBackTexture.Width * Main.hotbarScale[num159] && Main.mouseState.Y >= num161 && (float)Main.mouseState.Y <= (float)num161 + (float)Main.inventoryBackTexture.Height * Main.hotbarScale[num159] && !Main.player[Main.myPlayer].channel)
					{
						if (!Main.player[Main.myPlayer].hbLocked)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
						}
						if (Main.mouseState.LeftButton == ButtonState.Pressed && !Main.player[Main.myPlayer].hbLocked)
						{
							Main.player[Main.myPlayer].changeItem = num159;
						}
						Main.player[Main.myPlayer].showItemIcon = false;
						text5 = Main.player[Main.myPlayer].inventory[num159].name;
						if (Main.player[Main.myPlayer].inventory[num159].stack > 1)
						{
							object obj = text5;
							text5 = string.Concat(new object[]
							{
								obj, 
								" (", 
								Main.player[Main.myPlayer].inventory[num159].stack, 
								")"
							});
						}
						rare = Main.player[Main.myPlayer].inventory[num159].rare;
					}
					if (Main.player[Main.myPlayer].inventory[num159].type > 0 && Main.player[Main.myPlayer].inventory[num159].stack > 0)
					{
						num158 = 1f;
						if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height > 32)
						{
							if (Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width > Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height)
							{
								num158 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width;
							}
							else
							{
								num158 = 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height;
							}
						}
						num158 *= num160;
						SpriteBatch arg_CA01_0 = this.spriteBatch;
						Texture2D arg_CA01_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type];
						Vector2 arg_CA01_2 = new Vector2((float)num157 + 26f * num160 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width * 0.5f * num158, (float)num161 + 26f * num160 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height * 0.5f * num158);
						Rectangle? arg_CA01_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height));
						Color arg_CA01_4 = Main.player[Main.myPlayer].inventory[num159].GetAlpha(color9);
						float arg_CA01_5 = 0f;
						origin = default(Vector2);
						arg_CA01_0.Draw(arg_CA01_1, arg_CA01_2, arg_CA01_3, arg_CA01_4, arg_CA01_5, origin, num158, SpriteEffects.None, 0f);
						Color arg_CA2C_0 = Main.player[Main.myPlayer].inventory[num159].color;
						Color b = default(Color);
						if (arg_CA2C_0 != b)
						{
							SpriteBatch arg_CB5A_0 = this.spriteBatch;
							Texture2D arg_CB5A_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type];
							Vector2 arg_CB5A_2 = new Vector2((float)num157 + 26f * num160 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width * 0.5f * num158, (float)num161 + 26f * num160 - (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height * 0.5f * num158);
							Rectangle? arg_CB5A_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[num159].type].Height));
							Color arg_CB5A_4 = Main.player[Main.myPlayer].inventory[num159].GetColor(color9);
							float arg_CB5A_5 = 0f;
							origin = default(Vector2);
							arg_CB5A_0.Draw(arg_CB5A_1, arg_CB5A_2, arg_CB5A_3, arg_CB5A_4, arg_CB5A_5, origin, num158, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num159].stack > 1)
						{
							SpriteBatch arg_CBE1_0 = this.spriteBatch;
							SpriteFont arg_CBE1_1 = Main.fontItemStack;
							string arg_CBE1_2 = string.Concat(Main.player[Main.myPlayer].inventory[num159].stack);
							Vector2 arg_CBE1_3 = new Vector2((float)num157 + 10f * num160, (float)num161 + 26f * num160);
							Color arg_CBE1_4 = color9;
							float arg_CBE1_5 = 0f;
							origin = default(Vector2);
							arg_CBE1_0.DrawString(arg_CBE1_1, arg_CBE1_2, arg_CBE1_3, arg_CBE1_4, arg_CBE1_5, origin, num158, SpriteEffects.None, 0f);
						}
						if (Main.player[Main.myPlayer].inventory[num159].useAmmo > 0)
						{
							int useAmmo = Main.player[Main.myPlayer].inventory[num159].useAmmo;
							int num162 = 0;
							for (int num163 = 0; num163 < 40; num163++)
							{
								if (num163 < 4 && Main.player[Main.myPlayer].ammo[num163].ammo == useAmmo)
								{
									num162 += Main.player[Main.myPlayer].ammo[num163].stack;
								}
								if (Main.player[Main.myPlayer].inventory[num163].ammo == useAmmo)
								{
									num162 += Main.player[Main.myPlayer].inventory[num163].stack;
								}
							}
							SpriteBatch arg_CD03_0 = this.spriteBatch;
							SpriteFont arg_CD03_1 = Main.fontItemStack;
							string arg_CD03_2 = string.Concat(num162);
							Vector2 arg_CD03_3 = new Vector2((float)num157 + 8f * num160, (float)num161 + 30f * num160);
							Color arg_CD03_4 = color9;
							float arg_CD03_5 = 0f;
							origin = default(Vector2);
							arg_CD03_0.DrawString(arg_CD03_1, arg_CD03_2, arg_CD03_3, arg_CD03_4, arg_CD03_5, origin, num160 * 0.8f, SpriteEffects.None, 0f);
						}
						string text16 = string.Concat(num159 + 1);
						if (text16 == "10")
						{
							text16 = "0";
						}
						SpriteBatch arg_CDA7_0 = this.spriteBatch;
						SpriteFont arg_CDA7_1 = Main.fontItemStack;
						string arg_CDA7_2 = text16;
						Vector2 arg_CDA7_3 = new Vector2((float)num157 + 8f * Main.hotbarScale[num159], (float)num161 + 4f * Main.hotbarScale[num159]);
						Color arg_CDA7_4 = new Color((int)(color9.R / 2), (int)(color9.G / 2), (int)(color9.B / 2), (int)(color9.A / 2));
						float arg_CDA7_5 = 0f;
						origin = default(Vector2);
						arg_CDA7_0.DrawString(arg_CDA7_1, arg_CDA7_2, arg_CDA7_3, arg_CDA7_4, arg_CDA7_5, origin, num158, SpriteEffects.None, 0f);
						if (Main.player[Main.myPlayer].inventory[num159].potion)
						{
							Color alpha = Main.player[Main.myPlayer].inventory[num159].GetAlpha(color9);
							float num164 = (float)Main.player[Main.myPlayer].potionDelay / (float)Item.potionDelay;
							float num165 = (float)alpha.R * num164;
							float num166 = (float)alpha.G * num164;
							float num167 = (float)alpha.B * num164;
							float num168 = (float)alpha.A * num164;
							alpha = new Color((int)((byte)num165), (int)((byte)num166), (int)((byte)num167), (int)((byte)num168));
							SpriteBatch arg_CEE1_0 = this.spriteBatch;
							Texture2D arg_CEE1_1 = Main.cdTexture;
							Vector2 arg_CEE1_2 = new Vector2((float)num157 + 26f * Main.hotbarScale[num159] - (float)Main.cdTexture.Width * 0.5f * num158, (float)num161 + 26f * Main.hotbarScale[num159] - (float)Main.cdTexture.Height * 0.5f * num158);
							Rectangle? arg_CEE1_3 = new Rectangle?(new Rectangle(0, 0, Main.cdTexture.Width, Main.cdTexture.Height));
							Color arg_CEE1_4 = alpha;
							float arg_CEE1_5 = 0f;
							origin = default(Vector2);
							arg_CEE1_0.Draw(arg_CEE1_1, arg_CEE1_2, arg_CEE1_3, arg_CEE1_4, arg_CEE1_5, origin, num158, SpriteEffects.None, 0f);
						}
					}
					num157 += (int)((float)Main.inventoryBackTexture.Width * Main.hotbarScale[num159]) + 4;
				}
			}
			if (text5 != null && text5 != "" && Main.mouseItem.type == 0)
			{
				Main.player[Main.myPlayer].showItemIcon = false;
				this.MouseText(text5, rare, 0);
				flag = true;
			}
			if (Main.chatMode)
			{
				this.textBlinkerCount++;
				if (this.textBlinkerCount >= 20)
				{
					if (this.textBlinkerState == 0)
					{
						this.textBlinkerState = 1;
					}
					else
					{
						this.textBlinkerState = 0;
					}
					this.textBlinkerCount = 0;
				}
				string text17 = Main.chatText;
				if (this.textBlinkerState == 1)
				{
					text17 += "|";
				}
				SpriteBatch arg_D016_0 = this.spriteBatch;
				Texture2D arg_D016_1 = Main.textBackTexture;
				Vector2 arg_D016_2 = new Vector2(78f, (float)(Main.screenHeight - 36));
				Rectangle? arg_D016_3 = new Rectangle?(new Rectangle(0, 0, Main.textBackTexture.Width, Main.textBackTexture.Height));
				Color arg_D016_4 = new Color(100, 100, 100, 100);
				float arg_D016_5 = 0f;
				origin = default(Vector2);
				arg_D016_0.Draw(arg_D016_1, arg_D016_2, arg_D016_3, arg_D016_4, arg_D016_5, origin, 1f, SpriteEffects.None, 0f);
				for (int num169 = 0; num169 < 5; num169++)
				{
					int num170 = 0;
					int num171 = 0;
					Color black2 = Color.Black;
					if (num169 == 0)
					{
						num170 = -2;
					}
					if (num169 == 1)
					{
						num170 = 2;
					}
					if (num169 == 2)
					{
						num171 = -2;
					}
					if (num169 == 3)
					{
						num171 = 2;
					}
					if (num169 == 4)
					{
						black2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					}
					SpriteBatch arg_D0B5_0 = this.spriteBatch;
					SpriteFont arg_D0B5_1 = Main.fontMouseText;
					string arg_D0B5_2 = text17;
					Vector2 arg_D0B5_3 = new Vector2((float)(88 + num170), (float)(Main.screenHeight - 30 + num171));
					Color arg_D0B5_4 = black2;
					float arg_D0B5_5 = 0f;
					origin = default(Vector2);
					arg_D0B5_0.DrawString(arg_D0B5_1, arg_D0B5_2, arg_D0B5_3, arg_D0B5_4, arg_D0B5_5, origin, 1f, SpriteEffects.None, 0f);
				}
			}
			for (int num172 = 0; num172 < Main.numChatLines; num172++)
			{
				if (Main.chatMode || Main.chatLine[num172].showTime > 0)
				{
					float num173 = (float)Main.mouseTextColor / 255f;
					for (int num174 = 0; num174 < 5; num174++)
					{
						int num175 = 0;
						int num176 = 0;
						Color black3 = Color.Black;
						if (num174 == 0)
						{
							num175 = -2;
						}
						if (num174 == 1)
						{
							num175 = 2;
						}
						if (num174 == 2)
						{
							num176 = -2;
						}
						if (num174 == 3)
						{
							num176 = 2;
						}
						if (num174 == 4)
						{
							black3 = new Color((int)((byte)((float)Main.chatLine[num172].color.R * num173)), (int)((byte)((float)Main.chatLine[num172].color.G * num173)), (int)((byte)((float)Main.chatLine[num172].color.B * num173)), (int)Main.mouseTextColor);
						}
						SpriteBatch arg_D214_0 = this.spriteBatch;
						SpriteFont arg_D214_1 = Main.fontMouseText;
						string arg_D214_2 = Main.chatLine[num172].text;
						Vector2 arg_D214_3 = new Vector2((float)(88 + num175), (float)(Main.screenHeight - 30 + num176 - 28 - num172 * 21));
						Color arg_D214_4 = black3;
						float arg_D214_5 = 0f;
						origin = default(Vector2);
						arg_D214_0.DrawString(arg_D214_1, arg_D214_2, arg_D214_3, arg_D214_4, arg_D214_5, origin, 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.player[Main.myPlayer].dead)
			{
				string text18 = "You were slain...";
				SpriteBatch arg_D2CB_0 = this.spriteBatch;
				SpriteFont arg_D2CB_1 = Main.fontDeathText;
				string arg_D2CB_2 = text18;
				Vector2 arg_D2CB_3 = new Vector2((float)(Main.screenWidth / 2 - text18.Length * 10), (float)(Main.screenHeight / 2 - 20));
				Color arg_D2CB_4 = Main.player[Main.myPlayer].GetDeathAlpha(new Color(0, 0, 0, 0));
				float arg_D2CB_5 = 0f;
				origin = default(Vector2);
				arg_D2CB_0.DrawString(arg_D2CB_1, arg_D2CB_2, arg_D2CB_3, arg_D2CB_4, arg_D2CB_5, origin, 1f, SpriteEffects.None, 0f);
			}
			SpriteBatch arg_D38B_0 = this.spriteBatch;
			Texture2D arg_D38B_1 = Main.cursorTexture;
			Vector2 arg_D38B_2 = new Vector2((float)(Main.mouseState.X + 1), (float)(Main.mouseState.Y + 1));
			Rectangle? arg_D38B_3 = new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height));
			Color arg_D38B_4 = new Color((int)((float)Main.cursorColor.R * 0.2f), (int)((float)Main.cursorColor.G * 0.2f), (int)((float)Main.cursorColor.B * 0.2f), (int)((float)Main.cursorColor.A * 0.5f));
			float arg_D38B_5 = 0f;
			origin = default(Vector2);
			arg_D38B_0.Draw(arg_D38B_1, arg_D38B_2, arg_D38B_3, arg_D38B_4, arg_D38B_5, origin, Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
			SpriteBatch arg_D3F9_0 = this.spriteBatch;
			Texture2D arg_D3F9_1 = Main.cursorTexture;
			Vector2 arg_D3F9_2 = new Vector2((float)Main.mouseState.X, (float)Main.mouseState.Y);
			Rectangle? arg_D3F9_3 = new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height));
			Color arg_D3F9_4 = Main.cursorColor;
			float arg_D3F9_5 = 0f;
			origin = default(Vector2);
			arg_D3F9_0.Draw(arg_D3F9_1, arg_D3F9_2, arg_D3F9_3, arg_D3F9_4, arg_D3F9_5, origin, Main.cursorScale, SpriteEffects.None, 0f);
			if (Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)
			{
				Main.player[Main.myPlayer].showItemIcon = false;
				Main.player[Main.myPlayer].showItemIcon2 = 0;
				flag = true;
				float num177 = 1f;
				if (Main.itemTexture[Main.mouseItem.type].Width > 32 || Main.itemTexture[Main.mouseItem.type].Height > 32)
				{
					if (Main.itemTexture[Main.mouseItem.type].Width > Main.itemTexture[Main.mouseItem.type].Height)
					{
						num177 = 32f / (float)Main.itemTexture[Main.mouseItem.type].Width;
					}
					else
					{
						num177 = 32f / (float)Main.itemTexture[Main.mouseItem.type].Height;
					}
				}
				float num178 = 1f;
				Color white14 = Color.White;
				num177 *= num178;
				SpriteBatch arg_D5F7_0 = this.spriteBatch;
				Texture2D arg_D5F7_1 = Main.itemTexture[Main.mouseItem.type];
				Vector2 arg_D5F7_2 = new Vector2((float)Main.mouseState.X + 26f * num178 - (float)Main.itemTexture[Main.mouseItem.type].Width * 0.5f * num177, (float)Main.mouseState.Y + 26f * num178 - (float)Main.itemTexture[Main.mouseItem.type].Height * 0.5f * num177);
				Rectangle? arg_D5F7_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.mouseItem.type].Width, Main.itemTexture[Main.mouseItem.type].Height));
				Color arg_D5F7_4 = Main.mouseItem.GetAlpha(white14);
				float arg_D5F7_5 = 0f;
				origin = default(Vector2);
				arg_D5F7_0.Draw(arg_D5F7_1, arg_D5F7_2, arg_D5F7_3, arg_D5F7_4, arg_D5F7_5, origin, num177, SpriteEffects.None, 0f);
				Color arg_D614_0 = Main.mouseItem.color;
				Color b = default(Color);
				if (arg_D614_0 != b)
				{
					SpriteBatch arg_D70A_0 = this.spriteBatch;
					Texture2D arg_D70A_1 = Main.itemTexture[Main.mouseItem.type];
					Vector2 arg_D70A_2 = new Vector2((float)Main.mouseState.X + 26f * num178 - (float)Main.itemTexture[Main.mouseItem.type].Width * 0.5f * num177, (float)Main.mouseState.Y + 26f * num178 - (float)Main.itemTexture[Main.mouseItem.type].Height * 0.5f * num177);
					Rectangle? arg_D70A_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.mouseItem.type].Width, Main.itemTexture[Main.mouseItem.type].Height));
					Color arg_D70A_4 = Main.mouseItem.GetColor(white14);
					float arg_D70A_5 = 0f;
					origin = default(Vector2);
					arg_D70A_0.Draw(arg_D70A_1, arg_D70A_2, arg_D70A_3, arg_D70A_4, arg_D70A_5, origin, num177, SpriteEffects.None, 0f);
				}
				if (Main.mouseItem.stack > 1)
				{
					SpriteBatch arg_D78D_0 = this.spriteBatch;
					SpriteFont arg_D78D_1 = Main.fontItemStack;
					string arg_D78D_2 = string.Concat(Main.mouseItem.stack);
					Vector2 arg_D78D_3 = new Vector2((float)Main.mouseState.X + 10f * num178, (float)Main.mouseState.Y + 26f * num178);
					Color arg_D78D_4 = white14;
					float arg_D78D_5 = 0f;
					origin = default(Vector2);
					arg_D78D_0.DrawString(arg_D78D_1, arg_D78D_2, arg_D78D_3, arg_D78D_4, arg_D78D_5, origin, num177, SpriteEffects.None, 0f);
				}
			}
			Rectangle rectangle2 = new Rectangle((int)((float)Main.mouseState.X + Main.screenPosition.X), (int)((float)Main.mouseState.Y + Main.screenPosition.Y), 1, 1);
			if (!flag)
			{
				int num179 = 26 * Main.player[Main.myPlayer].statLifeMax / num12;
				int num180 = 0;
				if (Main.player[Main.myPlayer].statLifeMax > 200)
				{
					num179 = 260;
					num180 += 26;
				}
				if (Main.mouseState.X > 500 + num10 && Main.mouseState.X < 500 + num179 + num10 && Main.mouseState.Y > 32 && Main.mouseState.Y < 32 + Main.heartTexture.Height + num180)
				{
					Main.player[Main.myPlayer].showItemIcon = false;
					string cursorText = Main.player[Main.myPlayer].statLife + "/" + Main.player[Main.myPlayer].statLifeMax;
					this.MouseText(cursorText, 0, 0);
					flag = true;
				}
			}
			if (!flag)
			{
				int num181 = 24;
				int num182 = 28 * Main.player[Main.myPlayer].statManaMax2 / num18;
				if (Main.mouseState.X > 762 + num10 && Main.mouseState.X < 762 + num181 + num10 && Main.mouseState.Y > 30 && Main.mouseState.Y < 30 + num182)
				{
					Main.player[Main.myPlayer].showItemIcon = false;
					string cursorText2 = Main.player[Main.myPlayer].statMana + "/" + Main.player[Main.myPlayer].statManaMax2;
					this.MouseText(cursorText2, 0, 0);
					flag = true;
				}
			}
			if (!flag)
			{
				for (int num183 = 0; num183 < 200; num183++)
				{
					if (Main.item[num183].active)
					{
						Rectangle value2 = new Rectangle((int)((double)Main.item[num183].position.X + (double)Main.item[num183].width * 0.5 - (double)Main.itemTexture[Main.item[num183].type].Width * 0.5), (int)(Main.item[num183].position.Y + (float)Main.item[num183].height - (float)Main.itemTexture[Main.item[num183].type].Height), Main.itemTexture[Main.item[num183].type].Width, Main.itemTexture[Main.item[num183].type].Height);
						if (rectangle2.Intersects(value2))
						{
							Main.player[Main.myPlayer].showItemIcon = false;
							string text19 = Main.item[num183].name;
							if (Main.item[num183].stack > 1)
							{
								object obj = text19;
								text19 = string.Concat(new object[]
								{
									obj, 
									" (", 
									Main.item[num183].stack, 
									")"
								});
							}
							if (Main.item[num183].owner < 255 && Main.showItemOwner)
							{
								text19 = text19 + " <" + Main.player[Main.item[num183].owner].name + ">";
							}
							rare = Main.item[num183].rare;
							this.MouseText(text19, rare, 0);
							flag = true;
							break;
						}
					}
				}
			}
			for (int num184 = 0; num184 < 255; num184++)
			{
				if (Main.player[num184].active && Main.myPlayer != num184 && !Main.player[num184].dead)
				{
					Rectangle value3 = new Rectangle((int)((double)Main.player[num184].position.X + (double)Main.player[num184].width * 0.5 - 16.0), (int)(Main.player[num184].position.Y + (float)Main.player[num184].height - 48f), 32, 48);
					if (!flag && rectangle2.Intersects(value3))
					{
						Main.player[Main.myPlayer].showItemIcon = false;
						string text20 = string.Concat(new object[]
						{
							Main.player[num184].name, 
							": ", 
							Main.player[num184].statLife, 
							"/", 
							Main.player[num184].statLifeMax
						});
						if (Main.player[num184].hostile)
						{
							text20 += " (PvP)";
						}
						this.MouseText(text20, -1, Main.player[num184].difficulty);
					}
				}
			}
			if (!flag)
			{
				for (int num185 = 0; num185 < 1000; num185++)
				{
					if (Main.npc[num185].active)
					{
						Rectangle value4 = new Rectangle((int)((double)Main.npc[num185].position.X + (double)Main.npc[num185].width * 0.5 - (double)Main.npcTexture[Main.npc[num185].type].Width * 0.5), (int)(Main.npc[num185].position.Y + (float)Main.npc[num185].height - (float)(Main.npcTexture[Main.npc[num185].type].Height / Main.npcFrameCount[Main.npc[num185].type])), Main.npcTexture[Main.npc[num185].type].Width, Main.npcTexture[Main.npc[num185].type].Height / Main.npcFrameCount[Main.npc[num185].type]);
						if (rectangle2.Intersects(value4))
						{
							bool flag7 = false;
							if (Main.npc[num185].townNPC)
							{
								Rectangle rectangle3 = new Rectangle((int)(Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Player.tileRangeX * 16)), (int)(Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
								Rectangle value5 = new Rectangle((int)Main.npc[num185].position.X, (int)Main.npc[num185].position.Y, Main.npc[num185].width, Main.npc[num185].height);
								if (rectangle3.Intersects(value5))
								{
									flag7 = true;
								}
							}
							if (flag7 && !Main.player[Main.myPlayer].dead)
							{
								int num186 = -(Main.npc[num185].width / 2 + 8);
								SpriteEffects effects2 = SpriteEffects.None;
								if (Main.npc[num185].spriteDirection == -1)
								{
									effects2 = SpriteEffects.FlipHorizontally;
									num186 = Main.npc[num185].width / 2 + 8;
								}
								SpriteBatch arg_E0F1_0 = this.spriteBatch;
								Texture2D arg_E0F1_1 = Main.chatTexture;
								Vector2 arg_E0F1_2 = new Vector2(Main.npc[num185].position.X + (float)(Main.npc[num185].width / 2) - Main.screenPosition.X - (float)(Main.chatTexture.Width / 2) - (float)num186, Main.npc[num185].position.Y - (float)Main.chatTexture.Height - Main.screenPosition.Y);
								Rectangle? arg_E0F1_3 = new Rectangle?(new Rectangle(0, 0, Main.chatTexture.Width, Main.chatTexture.Height));
								Color arg_E0F1_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
								float arg_E0F1_5 = 0f;
								origin = default(Vector2);
								arg_E0F1_0.Draw(arg_E0F1_1, arg_E0F1_2, arg_E0F1_3, arg_E0F1_4, arg_E0F1_5, origin, 1f, effects2, 0f);
								if (Main.mouseState.RightButton == ButtonState.Pressed && Main.npcChatRelease)
								{
									Main.npcChatRelease = false;
									if (Main.player[Main.myPlayer].talkNPC != num185)
									{
										Main.player[Main.myPlayer].sign = -1;
										Main.editSign = false;
										Main.player[Main.myPlayer].talkNPC = num185;
										Main.playerInventory = false;
										Main.player[Main.myPlayer].chest = -1;
										Main.npcChatText = Main.npc[num185].GetChat();
										Main.PlaySound(24, -1, -1, 1);
									}
								}
							}
							Main.player[Main.myPlayer].showItemIcon = false;
							string text21 = Main.npc[num185].name;
							if (Main.npc[num185].lifeMax > 1 && !Main.npc[num185].dontTakeDamage)
							{
								object obj = text21;
								text21 = string.Concat(new object[]
								{
									obj, 
									": ", 
									Main.npc[num185].life, 
									"/", 
									Main.npc[num185].lifeMax
								});
							}
							this.MouseText(text21, 0, 0);
							break;
						}
					}
				}
			}
			if (Main.mouseState.RightButton == ButtonState.Pressed)
			{
				Main.npcChatRelease = false;
			}
			else
			{
				Main.npcChatRelease = true;
			}
			if (Main.player[Main.myPlayer].showItemIcon && (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type > 0 || Main.player[Main.myPlayer].showItemIcon2 > 0))
			{
				int num187 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
				Color color10 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].GetAlpha(Color.White);
				Color color11 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].GetColor(Color.White);
				if (Main.player[Main.myPlayer].showItemIcon2 > 0)
				{
					num187 = Main.player[Main.myPlayer].showItemIcon2;
					color10 = Color.White;
					color11 = default(Color);
				}
				SpriteBatch arg_E41A_0 = this.spriteBatch;
				Texture2D arg_E41A_1 = Main.itemTexture[num187];
				Vector2 arg_E41A_2 = new Vector2((float)(Main.mouseState.X + 10), (float)(Main.mouseState.Y + 10));
				Rectangle? arg_E41A_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[num187].Width, Main.itemTexture[num187].Height));
				Color arg_E41A_4 = color10;
				float arg_E41A_5 = 0f;
				origin = default(Vector2);
				arg_E41A_0.Draw(arg_E41A_1, arg_E41A_2, arg_E41A_3, arg_E41A_4, arg_E41A_5, origin, 1f, SpriteEffects.None, 0f);
				if (Main.player[Main.myPlayer].showItemIcon2 == 0)
				{
					Color arg_E468_0 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].color;
					Color b = default(Color);
					if (arg_E468_0 != b)
					{
						SpriteBatch arg_E555_0 = this.spriteBatch;
						Texture2D arg_E555_1 = Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type];
						Vector2 arg_E555_2 = new Vector2((float)(Main.mouseState.X + 10), (float)(Main.mouseState.Y + 10));
						Rectangle? arg_E555_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type].Height));
						Color arg_E555_4 = color11;
						float arg_E555_5 = 0f;
						origin = default(Vector2);
						arg_E555_0.Draw(arg_E555_1, arg_E555_2, arg_E555_3, arg_E555_4, arg_E555_5, origin, 1f, SpriteEffects.None, 0f);
					}
				}
			}
			Main.player[Main.myPlayer].showItemIcon = false;
			Main.player[Main.myPlayer].showItemIcon2 = 0;
		}
		protected void QuitGame()
		{
			base.Exit();
		}
		protected void DrawMenu()
		{
			Star.UpdateStars();
			Cloud.UpdateClouds();
			Main.evilTiles = 0;
			Main.jungleTiles = 0;
			Main.chatMode = false;
			for (int i = 0; i < Main.numChatLines; i++)
			{
				Main.chatLine[i] = new ChatLine();
			}
			this.DrawFPS();
			Main.screenPosition.Y = (float)(Main.worldSurface * 16.0 - (double)Main.screenHeight);
			Main.background = 0;
            byte b = (byte)((255 + Main.tileColor.R * 2) / 3);
			Color color = new Color((int)b, (int)b, (int)b, 255);
			this.logoRotation += this.logoRotationSpeed * 3E-05f;
			if ((double)this.logoRotation > 0.1)
			{
				this.logoRotationDirection = -1f;
			}
			else
			{
				if ((double)this.logoRotation < -0.1)
				{
					this.logoRotationDirection = 1f;
				}
			}
			if (this.logoRotationSpeed < 20f & this.logoRotationDirection == 1f)
			{
				this.logoRotationSpeed += 1f;
			}
			else
			{
				if (this.logoRotationSpeed > -20f & this.logoRotationDirection == -1f)
				{
					this.logoRotationSpeed -= 1f;
				}
			}
			this.logoScale += this.logoScaleSpeed * 1E-05f;
			if ((double)this.logoScale > 1.1)
			{
				this.logoScaleDirection = -1f;
			}
			else
			{
				if ((double)this.logoScale < 0.9)
				{
					this.logoScaleDirection = 1f;
				}
			}
			if (this.logoScaleSpeed < 50f & this.logoScaleDirection == 1f)
			{
				this.logoScaleSpeed += 1f;
			}
			else
			{
				if (this.logoScaleSpeed > -50f & this.logoScaleDirection == -1f)
				{
					this.logoScaleSpeed -= 1f;
				}
			}
			this.spriteBatch.Draw(Main.logoTexture, new Vector2((float)(Main.screenWidth / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height)), color, this.logoRotation, new Vector2((float)(Main.logoTexture.Width / 2), (float)(Main.logoTexture.Height / 2)), this.logoScale, SpriteEffects.None, 0f);
			int num = 250;
			int num2 = Main.screenWidth / 2;
			int num3 = 80;
			int num4 = 0;
			int num5 = Main.menuMode;
			int num6 = -1;
			int num7 = 0;
			int num8 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num9 = 0;
			bool[] array = new bool[Main.maxMenuItems];
			bool[] array2 = new bool[Main.maxMenuItems];
			int[] array3 = new int[Main.maxMenuItems];
			int[] array4 = new int[Main.maxMenuItems];
			byte[] array5 = new byte[Main.maxMenuItems];
			float[] array6 = new float[Main.maxMenuItems];
			bool[] array7 = new bool[Main.maxMenuItems];
			for (int j = 0; j < Main.maxMenuItems; j++)
			{
				array[j] = false;
				array2[j] = false;
				array3[j] = 0;
				array4[j] = 0;
				array6[j] = 1f;
			}
			string[] array8 = new string[Main.maxMenuItems];
			if (Main.menuMode == -1)
			{
				Main.menuMode = 0;
			}
			if (Main.netMode == 2)
			{
				bool flag4 = true;
				for (int k = 0; k < 8; k++)
				{
					if (k < 255)
					{
						try
						{
							array8[k] = Netplay.serverSock[k].statusText;
							if (Netplay.serverSock[k].active && Main.showSpam)
							{
								string[] array9;
								string[] expr_38A = array9 = array8;
								IntPtr intPtr;
								int expr_38F = (int)(intPtr = (IntPtr)k);
								object obj = array9[(int)intPtr];
								expr_38A[expr_38F] = string.Concat(new object[]
								{
									obj, 
									" (", 
									NetMessage.buffer[k].spamCount, 
									")"
								});
							}
						}
						catch
						{
							array8[k] = "";
						}
						array[k] = true;
						if (array8[k] != "" && array8[k] != null)
						{
							flag4 = false;
						}
					}
				}
				if (flag4)
				{
					array8[0] = "Start a new instance of Freeria to join!";
					array8[1] = "Running on port " + Netplay.serverPort + ".";
				}
				num4 = 11;
				array8[9] = Main.statusText;
				array[9] = true;
				num = 170;
				num3 = 30;
				array3[10] = 20;
				array3[10] = 40;
				array8[10] = "Disconnect";
				if (this.selectedMenu == 10)
				{
					Netplay.disconnect = true;
					Main.PlaySound(11, -1, -1, 1);
				}
			}
			else
			{
				if (Main.menuMode == 31)
				{
					string password = Netplay.password;
					Netplay.password = Main.GetInputText(Netplay.password);
					if (password != Netplay.password)
					{
						Main.PlaySound(12, -1, -1, 1);
					}
					array8[0] = "Server Requires Password:";
					this.textBlinkerCount++;
					if (this.textBlinkerCount >= 20)
					{
						if (this.textBlinkerState == 0)
						{
							this.textBlinkerState = 1;
						}
						else
						{
							this.textBlinkerState = 0;
						}
						this.textBlinkerCount = 0;
					}
					array8[1] = Netplay.password;
					if (this.textBlinkerState == 1)
					{
						string[] array9;
						(array9 = array8)[1] = array9[1] + "|";
						array4[1] = 1;
					}
					else
					{
						string[] array9;
						(array9 = array8)[1] = array9[1] + " ";
					}
					array[0] = true;
					array[1] = true;
					array3[1] = -20;
					array3[2] = 20;
					array8[2] = "Accept";
					array8[3] = "Back";
					num4 = 4;
					if (this.selectedMenu == 3)
					{
						Main.PlaySound(11, -1, -1, 1);
						Main.menuMode = 0;
						Netplay.disconnect = true;
						Netplay.password = "";
					}
					else
					{
						if (this.selectedMenu == 2 || Main.inputTextEnter)
						{
							NetMessage.SendData(38, -1, -1, Netplay.password, 0, 0f, 0f, 0f, 0);
							Main.menuMode = 14;
						}
					}
				}
				else
				{
					if (Main.netMode == 1 || Main.menuMode == 14)
					{
						num4 = 2;
						array8[0] = Main.statusText;
						array[0] = true;
						num = 300;
						array8[1] = "Cancel";
						if (this.selectedMenu != 1)
						{
							goto IL_2F8D;
						}
						Netplay.disconnect = true;
						Netplay.clientSock.tcpClient.Close();
						Main.PlaySound(11, -1, -1, 1);
						Main.menuMode = 0;
						Main.netMode = 0;
						try
						{
							this.tServer.Kill();
							goto IL_2F8D;
						}
						catch
						{
							goto IL_2F8D;
						}
					}
					if (Main.menuMode == 30)
					{
						string password2 = Netplay.password;
						Netplay.password = Main.GetInputText(Netplay.password);
						if (password2 != Netplay.password)
						{
							Main.PlaySound(12, -1, -1, 1);
						}
						array8[0] = "Enter Server Password:";
						this.textBlinkerCount++;
						if (this.textBlinkerCount >= 20)
						{
							if (this.textBlinkerState == 0)
							{
								this.textBlinkerState = 1;
							}
							else
							{
								this.textBlinkerState = 0;
							}
							this.textBlinkerCount = 0;
						}
						array8[1] = Netplay.password;
						if (this.textBlinkerState == 1)
						{
							string[] array9;
							(array9 = array8)[1] = array9[1] + "|";
							array4[1] = 1;
						}
						else
						{
							string[] array9;
							(array9 = array8)[1] = array9[1] + " ";
						}
						array[0] = true;
						array[1] = true;
						array3[1] = -20;
						array3[2] = 20;
						array8[2] = "Accept";
						array8[3] = "Back";
						num4 = 4;
						if (this.selectedMenu == 3)
						{
							Main.PlaySound(11, -1, -1, 1);
							Main.menuMode = 6;
							Netplay.password = "";
						}
						else
						{
							if (this.selectedMenu == 2 || Main.inputTextEnter || Main.autoPass)
							{
								this.tServer.StartInfo.FileName = "FreeriaServer.exe";
								this.tServer.StartInfo.Arguments = string.Concat(new string[]
								{
									"-autoshutdown -world \"", 
									Main.worldPathName, 
									"\" -password \"", 
									Netplay.password, 
									"\""
								});
								if (Main.libPath != "")
								{
									ProcessStartInfo expr_810 = this.tServer.StartInfo;
									expr_810.Arguments = expr_810.Arguments + " -loadlib " + Main.libPath;
								}
								this.tServer.StartInfo.UseShellExecute = false;
								this.tServer.StartInfo.CreateNoWindow = true;
								this.tServer.Start();
								Netplay.SetIP("127.0.0.1");
								Main.autoPass = true;
								Main.statusText = "Starting server...";
								Netplay.StartClient();
								Main.menuMode = 10;
							}
						}
					}
					else
					{
						if (Main.menuMode == 15)
						{
							num4 = 2;
							array8[0] = Main.statusText;
							array[0] = true;
							num = 80;
							num3 = 400;
							array8[1] = "Back";
							if (this.selectedMenu == 1)
							{
								Netplay.disconnect = true;
								Main.PlaySound(11, -1, -1, 1);
								Main.menuMode = 0;
								Main.netMode = 0;
							}
						}
						else
						{
							if (Main.menuMode == 200)
							{
								num4 = 3;
								array8[0] = "Load failed!";
								array[0] = true;
								num -= 30;
								array3[1] = 70;
								array3[2] = 50;
								array8[1] = "Load Backup";
								array8[2] = "Cancel";
								if (this.selectedMenu == 1)
								{
									if (File.Exists(Main.worldPathName + ".bak"))
									{
										File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
										File.Delete(Main.worldPathName + ".bak");
										Main.PlaySound(10, -1, -1, 1);
										WorldGen.playWorld();
										Main.menuMode = 10;
									}
									else
									{
										Main.PlaySound(11, -1, -1, 1);
										Main.menuMode = 0;
										Main.netMode = 0;
									}
								}
								if (this.selectedMenu == 2)
								{
									Main.PlaySound(11, -1, -1, 1);
									Main.menuMode = 0;
									Main.netMode = 0;
								}
							}
							else
							{
								if (Main.menuMode == 201)
								{
									num4 = 3;
									array8[0] = "Load failed!";
									array[0] = true;
									array[1] = true;
									num -= 30;
									array3[1] = -30;
									array3[2] = 50;
									array8[1] = "No backup found";
									array8[2] = "Back";
									if (this.selectedMenu == 2)
									{
										Main.PlaySound(11, -1, -1, 1);
										Main.menuMode = 0;
										Main.netMode = 0;
									}
								}
								else
								{
									if (Main.menuMode == 10)
									{
										num4 = 1;
										array8[0] = Main.statusText;
										array[0] = true;
										num = 300;
									}
									else
									{
										if (Main.menuMode == 100)
										{
											num4 = 1;
											array8[0] = Main.statusText;
											array[0] = true;
											num = 300;
										}
										else
										{
											if (Main.menuMode == 0)
											{
												Main.menuMultiplayer = false;
												Main.menuServer = false;
												Main.netMode = 0;
												array8[0] = "Single Player";
												array8[1] = "Multiplayer";
												array8[2] = "Settings";
												array8[3] = "Exit";
												num4 = 4;
												if (this.selectedMenu == 3)
												{
													this.QuitGame();
												}
												if (this.selectedMenu == 1)
												{
													Main.PlaySound(10, -1, -1, 1);
													Main.menuMode = 12;
												}
												if (this.selectedMenu == 2)
												{
													Main.PlaySound(10, -1, -1, 1);
													Main.menuMode = 11;
												}
												if (this.selectedMenu == 0)
												{
													Main.PlaySound(10, -1, -1, 1);
													Main.menuMode = 1;
													Main.LoadPlayers();
												}
											}
											else
											{
												if (Main.menuMode == 1)
												{
													Main.myPlayer = 0;
													num = 190;
													num3 = 50;
													array8[5] = "Create Character";
													array8[6] = "Delete";
													if (Main.numLoadPlayers == 5)
													{
														array2[5] = true;
														array8[5] = "";
													}
													else
													{
														if (Main.numLoadPlayers == 0)
														{
															array2[6] = true;
															array8[6] = "";
														}
													}
													array8[7] = "Back";
													for (int l = 0; l < 5; l++)
													{
														if (l < Main.numLoadPlayers)
														{
															array8[l] = Main.loadPlayer[l].name;
															array5[l] = Main.loadPlayer[l].difficulty;
														}
														else
														{
															array8[l] = null;
														}
													}
													num4 = 8;
													if (this.focusMenu >= 0 && this.focusMenu < Main.numLoadPlayers)
													{
														num6 = this.focusMenu;
														Vector2 vector = Main.fontDeathText.MeasureString(array8[num6]);
														num7 = (int)((double)(Main.screenWidth / 2) + (double)vector.X * 0.5 + 10.0);
														num8 = num + num3 * this.focusMenu + 4;
													}
													if (this.selectedMenu == 7)
													{
														Main.autoJoin = false;
														Main.autoPass = false;
														Main.PlaySound(11, -1, -1, 1);
														if (Main.menuMultiplayer)
														{
															Main.menuMode = 12;
															Main.menuMultiplayer = false;
															Main.menuServer = false;
														}
														else
														{
															Main.menuMode = 0;
														}
													}
													else
													{
														if (this.selectedMenu == 5)
														{
															Main.loadPlayer[Main.numLoadPlayers] = new Player();
															Main.PlaySound(10, -1, -1, 1);
															Main.menuMode = 2;
														}
														else
														{
															if (this.selectedMenu == 6)
															{
																Main.PlaySound(10, -1, -1, 1);
																Main.menuMode = 4;
															}
															else
															{
																if (this.selectedMenu >= 0)
																{
																	if (Main.menuMultiplayer)
																	{
																		this.selectedPlayer = this.selectedMenu;
																		Main.player[Main.myPlayer] = (Player)Main.loadPlayer[this.selectedPlayer].Clone();
																		Main.playerPathName = Main.loadPlayerPath[this.selectedPlayer];
																		Main.PlaySound(10, -1, -1, 1);
																		if (Main.autoJoin)
																		{
																			if (Netplay.SetIP(Main.getIP))
																			{
																				Main.menuMode = 10;
																				Netplay.StartClient();
																			}
																			else
																			{
																				if (Netplay.SetIP2(Main.getIP))
																				{
																					Main.menuMode = 10;
																					Netplay.StartClient();
																				}
																			}
																			Main.autoJoin = false;
																		}
																		else
																		{
																			if (Main.menuServer)
																			{
																				Main.LoadWorlds();
																				Main.menuMode = 6;
																			}
																			else
																			{
																				Main.menuMode = 13;
																			}
																		}
																	}
																	else
																	{
																		Main.myPlayer = 0;
																		this.selectedPlayer = this.selectedMenu;
																		Main.player[Main.myPlayer] = (Player)Main.loadPlayer[this.selectedPlayer].Clone();
																		Main.playerPathName = Main.loadPlayerPath[this.selectedPlayer];
																		Main.LoadWorlds();
																		Main.PlaySound(10, -1, -1, 1);
																		Main.menuMode = 6;
																	}
																}
															}
														}
													}
												}
												else
												{
													if (Main.menuMode == 2)
													{
														if (this.selectedMenu == 0)
														{
															Main.menuMode = 17;
															Main.PlaySound(10, -1, -1, 1);
															this.selColor = Main.loadPlayer[Main.numLoadPlayers].hairColor;
														}
														if (this.selectedMenu == 1)
														{
															Main.menuMode = 18;
															Main.PlaySound(10, -1, -1, 1);
															this.selColor = Main.loadPlayer[Main.numLoadPlayers].eyeColor;
														}
														if (this.selectedMenu == 2)
														{
															Main.menuMode = 19;
															Main.PlaySound(10, -1, -1, 1);
															this.selColor = Main.loadPlayer[Main.numLoadPlayers].skinColor;
														}
														if (this.selectedMenu == 3)
														{
															Main.menuMode = 20;
															Main.PlaySound(10, -1, -1, 1);
														}
														array8[0] = "Hair";
														array8[1] = "Eyes";
														array8[2] = "Skin";
														array8[3] = "Clothes";
														num = 246;
														for (int m = 0; m < 6; m++)
														{
															array6[m] = 0.8f;
														}
														num3 = 40;
														array3[6] = 16;
														array3[7] = 25;
														num6 = Main.numLoadPlayers;
														num7 = Main.screenWidth / 2 - 16;
														num8 = 200;
														if (Main.loadPlayer[num6].male)
														{
															array8[4] = "Male";
														}
														else
														{
															array8[4] = "Female";
														}
														if (this.selectedMenu == 4)
														{
															if (Main.loadPlayer[num6].male)
															{
																Main.PlaySound(20, -1, -1, 1);
																Main.loadPlayer[num6].male = false;
															}
															else
															{
																Main.PlaySound(1, -1, -1, 1);
																Main.loadPlayer[num6].male = true;
															}
														}
														if (Main.loadPlayer[num6].difficulty == 2)
														{
															array8[5] = "Hardcore";
															array5[5] = Main.loadPlayer[num6].difficulty;
														}
														else
														{
															if (Main.loadPlayer[num6].difficulty == 1)
															{
																array8[5] = "Mediumcore";
																array5[5] = Main.loadPlayer[num6].difficulty;
															}
															else
															{
																array8[5] = "Softcore";
															}
														}
														if (this.selectedMenu == 5)
														{
															Main.PlaySound(10, -1, -1, 1);
															Main.menuMode = 222;
														}
														array8[6] = "Create";
														array8[7] = "Back";
														num4 = 8;
														if (this.selectedMenu == 7)
														{
															Main.PlaySound(11, -1, -1, 1);
															Main.menuMode = 1;
														}
														else
														{
															if (this.selectedMenu == 6)
															{
																Main.PlaySound(10, -1, -1, 1);
																Main.loadPlayer[Main.numLoadPlayers].name = "";
																Main.menuMode = 3;
															}
														}
													}
													else
													{
														if (Main.menuMode == 222)
														{
															if (this.focusMenu == 3)
															{
																array8[0] = "Hardcore characters die for good";
															}
															else
															{
																if (this.focusMenu == 2)
																{
																	array8[0] = "Mediumcore characters drop items on death";
																}
																else
																{
																	if (this.focusMenu == 1)
																	{
																		array8[0] = "Softcore characters drop money on death";
																	}
																	else
																	{
																		array8[0] = "Select difficulty";
																	}
																}
															}
															num3 = 50;
															array3[1] = 25;
															array3[2] = 25;
															array3[3] = 25;
															array[0] = true;
															array8[1] = "Softcore";
															array8[2] = "Mediumcore";
															array5[2] = 1;
															array8[3] = "Hardcore";
															array5[3] = 2;
															num4 = 4;
															if (this.selectedMenu == 1)
															{
																Main.loadPlayer[Main.numLoadPlayers].difficulty = 0;
																Main.menuMode = 2;
															}
															else
															{
																if (this.selectedMenu == 2)
																{
																	Main.menuMode = 2;
																	Main.loadPlayer[Main.numLoadPlayers].difficulty = 1;
																}
																else
																{
																	if (this.selectedMenu == 3)
																	{
																		Main.loadPlayer[Main.numLoadPlayers].difficulty = 2;
																		Main.menuMode = 2;
																	}
																}
															}
														}
														else
														{
															if (Main.menuMode == 20)
															{
																if (this.selectedMenu == 0)
																{
																	Main.menuMode = 21;
																	Main.PlaySound(10, -1, -1, 1);
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].shirtColor;
																}
																if (this.selectedMenu == 1)
																{
																	Main.menuMode = 22;
																	Main.PlaySound(10, -1, -1, 1);
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].underShirtColor;
																}
																if (this.selectedMenu == 2)
																{
																	Main.menuMode = 23;
																	Main.PlaySound(10, -1, -1, 1);
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].pantsColor;
																}
																if (this.selectedMenu == 3)
																{
																	this.selColor = Main.loadPlayer[Main.numLoadPlayers].shoeColor;
																	Main.menuMode = 24;
																	Main.PlaySound(10, -1, -1, 1);
																}
																array8[0] = "Shirt";
																array8[1] = "Undershirt";
																array8[2] = "Pants";
																array8[3] = "Shoes";
																num = 260;
																num3 = 50;
																array3[5] = 20;
																array8[5] = "Back";
																num4 = 6;
																num6 = Main.numLoadPlayers;
																num7 = Main.screenWidth / 2 - 16;
																num8 = 210;
																if (this.selectedMenu == 5)
																{
																	Main.PlaySound(11, -1, -1, 1);
																	Main.menuMode = 2;
																}
															}
															else
															{
																if (Main.menuMode == 17)
																{
																	num6 = Main.numLoadPlayers;
																	num7 = Main.screenWidth / 2 - 16;
																	num8 = 210;
																	flag = true;
																	num9 = 390;
																	num = 260;
																	num3 = 60;
																	Main.loadPlayer[num6].hairColor = this.selColor;
																	num4 = 3;
																	array8[0] = "Hair " + (Main.loadPlayer[num6].hair + 1);
																	array8[1] = "Hair Color";
																	array[1] = true;
																	array3[2] = 150;
																	array3[1] = 10;
																	array8[2] = "Back";
																	if (this.selectedMenu == 0)
																	{
																		Main.PlaySound(12, -1, -1, 1);
																		Main.loadPlayer[num6].hair++;
																		if (Main.loadPlayer[num6].hair >= 36)
																		{
																			Main.loadPlayer[num6].hair = 0;
																		}
																	}
																	else
																	{
																		if (this.selectedMenu2 == 0)
																		{
																			Main.PlaySound(12, -1, -1, 1);
																			Main.loadPlayer[num6].hair--;
																			if (Main.loadPlayer[num6].hair < 0)
																			{
																				Main.loadPlayer[num6].hair = 35;
																			}
																		}
																	}
																	if (this.selectedMenu == 2)
																	{
																		Main.menuMode = 2;
																		Main.PlaySound(11, -1, -1, 1);
																	}
																}
																else
																{
																	if (Main.menuMode == 18)
																	{
																		num6 = Main.numLoadPlayers;
																		num7 = Main.screenWidth / 2 - 16;
																		num8 = 210;
																		flag = true;
																		num9 = 370;
																		num = 240;
																		num3 = 60;
																		Main.loadPlayer[num6].eyeColor = this.selColor;
																		num4 = 3;
																		array8[0] = "";
																		array8[1] = "Eye Color";
																		array[1] = true;
																		array3[2] = 170;
																		array3[1] = 10;
																		array8[2] = "Back";
																		if (this.selectedMenu == 2)
																		{
																			Main.menuMode = 2;
																			Main.PlaySound(11, -1, -1, 1);
																		}
																	}
																	else
																	{
																		if (Main.menuMode == 19)
																		{
																			num6 = Main.numLoadPlayers;
																			num7 = Main.screenWidth / 2 - 16;
																			num8 = 210;
																			flag = true;
																			num9 = 370;
																			num = 240;
																			num3 = 60;
																			Main.loadPlayer[num6].skinColor = this.selColor;
																			num4 = 3;
																			array8[0] = "";
																			array8[1] = "Skin Color";
																			array[1] = true;
																			array3[2] = 170;
																			array3[1] = 10;
																			array8[2] = "Back";
																			if (this.selectedMenu == 2)
																			{
																				Main.menuMode = 2;
																				Main.PlaySound(11, -1, -1, 1);
																			}
																		}
																		else
																		{
																			if (Main.menuMode == 21)
																			{
																				num6 = Main.numLoadPlayers;
																				num7 = Main.screenWidth / 2 - 16;
																				num8 = 210;
																				flag = true;
																				num9 = 370;
																				num = 240;
																				num3 = 60;
																				Main.loadPlayer[num6].shirtColor = this.selColor;
																				num4 = 3;
																				array8[0] = "";
																				array8[1] = "Shirt Color";
																				array[1] = true;
																				array3[2] = 170;
																				array3[1] = 10;
																				array8[2] = "Back";
																				if (this.selectedMenu == 2)
																				{
																					Main.menuMode = 20;
																					Main.PlaySound(11, -1, -1, 1);
																				}
																			}
																			else
																			{
																				if (Main.menuMode == 22)
																				{
																					num6 = Main.numLoadPlayers;
																					num7 = Main.screenWidth / 2 - 16;
																					num8 = 210;
																					flag = true;
																					num9 = 370;
																					num = 240;
																					num3 = 60;
																					Main.loadPlayer[num6].underShirtColor = this.selColor;
																					num4 = 3;
																					array8[0] = "";
																					array8[1] = "Undershirt Color";
																					array[1] = true;
																					array3[2] = 170;
																					array3[1] = 10;
																					array8[2] = "Back";
																					if (this.selectedMenu == 2)
																					{
																						Main.menuMode = 20;
																						Main.PlaySound(11, -1, -1, 1);
																					}
																				}
																				else
																				{
																					if (Main.menuMode == 23)
																					{
																						num6 = Main.numLoadPlayers;
																						num7 = Main.screenWidth / 2 - 16;
																						num8 = 210;
																						flag = true;
																						num9 = 370;
																						num = 240;
																						num3 = 60;
																						Main.loadPlayer[num6].pantsColor = this.selColor;
																						num4 = 3;
																						array8[0] = "";
																						array8[1] = "Pants Color";
																						array[1] = true;
																						array3[2] = 170;
																						array3[1] = 10;
																						array8[2] = "Back";
																						if (this.selectedMenu == 2)
																						{
																							Main.menuMode = 20;
																							Main.PlaySound(11, -1, -1, 1);
																						}
																					}
																					else
																					{
																						if (Main.menuMode == 24)
																						{
																							num6 = Main.numLoadPlayers;
																							num7 = Main.screenWidth / 2 - 16;
																							num8 = 210;
																							flag = true;
																							num9 = 370;
																							num = 240;
																							num3 = 60;
																							Main.loadPlayer[num6].shoeColor = this.selColor;
																							num4 = 3;
																							array8[0] = "";
																							array8[1] = "Shoe Color";
																							array[1] = true;
																							array3[2] = 170;
																							array3[1] = 10;
																							array8[2] = "Back";
																							if (this.selectedMenu == 2)
																							{
																								Main.menuMode = 20;
																								Main.PlaySound(11, -1, -1, 1);
																							}
																						}
																						else
																						{
																							if (Main.menuMode == 3)
																							{
																								string name = Main.loadPlayer[Main.numLoadPlayers].name;
																								Main.loadPlayer[Main.numLoadPlayers].name = Main.GetInputText(Main.loadPlayer[Main.numLoadPlayers].name);
																								if (Main.loadPlayer[Main.numLoadPlayers].name.Length > Player.nameLen)
																								{
																									Main.loadPlayer[Main.numLoadPlayers].name = Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, Player.nameLen);
																								}
																								if (name != Main.loadPlayer[Main.numLoadPlayers].name)
																								{
																									Main.PlaySound(12, -1, -1, 1);
																								}
																								array8[0] = "Enter Character Name:";
																								array2[2] = true;
																								if (Main.loadPlayer[Main.numLoadPlayers].name != "")
																								{
																									if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, 1) == " ")
																									{
																										Main.loadPlayer[Main.numLoadPlayers].name = "";
																									}
																									for (int n = 0; n < Main.loadPlayer[Main.numLoadPlayers].name.Length; n++)
																									{
																										if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(n, 1) != " ")
																										{
																											array2[2] = false;
																										}
																									}
																								}
																								this.textBlinkerCount++;
																								if (this.textBlinkerCount >= 20)
																								{
																									if (this.textBlinkerState == 0)
																									{
																										this.textBlinkerState = 1;
																									}
																									else
																									{
																										this.textBlinkerState = 0;
																									}
																									this.textBlinkerCount = 0;
																								}
																								array8[1] = Main.loadPlayer[Main.numLoadPlayers].name;
																								if (this.textBlinkerState == 1)
																								{
																									string[] array9;
																									(array9 = array8)[1] = array9[1] + "|";
																									array4[1] = 1;
																								}
																								else
																								{
																									string[] array9;
																									(array9 = array8)[1] = array9[1] + " ";
																								}
																								array[0] = true;
																								array[1] = true;
																								array3[1] = -20;
																								array3[2] = 20;
																								array8[2] = "Accept";
																								array8[3] = "Back";
																								num4 = 4;
																								if (this.selectedMenu == 3)
																								{
																									Main.PlaySound(11, -1, -1, 1);
																									Main.menuMode = 2;
																								}
																								if (this.selectedMenu == 2 || (!array2[2] && Main.inputTextEnter))
																								{
																									Main.loadPlayer[Main.numLoadPlayers].name.Trim();
																									Main.loadPlayerPath[Main.numLoadPlayers] = Main.nextLoadPlayer();
																									Player.SavePlayer(Main.loadPlayer[Main.numLoadPlayers], Main.loadPlayerPath[Main.numLoadPlayers]);
																									Main.LoadPlayers();
																									Main.PlaySound(10, -1, -1, 1);
																									Main.menuMode = 1;
																								}
																							}
																							else
																							{
																								if (Main.menuMode == 4)
																								{
																									num = 220;
																									num3 = 60;
																									array8[5] = "Back";
																									for (int num10 = 0; num10 < 5; num10++)
																									{
																										if (num10 < Main.numLoadPlayers)
																										{
																											array8[num10] = Main.loadPlayer[num10].name;
																											array5[num10] = Main.loadPlayer[num10].difficulty;
																										}
																										else
																										{
																											array8[num10] = null;
																										}
																									}
																									num4 = 6;
																									if (this.focusMenu >= 0 && this.focusMenu < Main.numLoadPlayers)
																									{
																										num6 = this.focusMenu;
																										Vector2 vector2 = Main.fontDeathText.MeasureString(array8[num6]);
																										num7 = (int)((double)(Main.screenWidth / 2) + (double)vector2.X * 0.5 + 10.0);
																										num8 = num + num3 * this.focusMenu + 4;
																									}
																									if (this.selectedMenu == 5)
																									{
																										Main.PlaySound(11, -1, -1, 1);
																										Main.menuMode = 1;
																									}
																									else
																									{
																										if (this.selectedMenu >= 0)
																										{
																											this.selectedPlayer = this.selectedMenu;
																											Main.PlaySound(10, -1, -1, 1);
																											Main.menuMode = 5;
																										}
																									}
																								}
																								else
																								{
																									if (Main.menuMode == 5)
																									{
																										array8[0] = "Delete " + Main.loadPlayer[this.selectedPlayer].name + "?";
																										array[0] = true;
																										array8[1] = "Yes";
																										array8[2] = "No";
																										num4 = 3;
																										if (this.selectedMenu == 1)
																										{
																											Main.ErasePlayer(this.selectedPlayer);
																											Main.PlaySound(10, -1, -1, 1);
																											Main.menuMode = 1;
																										}
																										else
																										{
																											if (this.selectedMenu == 2)
																											{
																												Main.PlaySound(11, -1, -1, 1);
																												Main.menuMode = 1;
																											}
																										}
																									}
																									else
																									{
																										if (Main.menuMode == 6)
																										{
																											num = 190;
																											num3 = 50;
																											array8[5] = "Create World";
																											array8[6] = "Delete";
																											if (Main.numLoadWorlds == 5)
																											{
																												array2[5] = true;
																												array8[5] = "";
																											}
																											else
																											{
																												if (Main.numLoadWorlds == 0)
																												{
																													array2[6] = true;
																													array8[6] = "";
																												}
																											}
																											array8[7] = "Back";
																											for (int num11 = 0; num11 < 5; num11++)
																											{
																												if (num11 < Main.numLoadWorlds)
																												{
																													array8[num11] = Main.loadWorld[num11];
																												}
																												else
																												{
																													array8[num11] = null;
																												}
																											}
																											num4 = 8;
																											if (this.selectedMenu == 7)
																											{
																												if (Main.menuMultiplayer)
																												{
																													Main.menuMode = 12;
																												}
																												else
																												{
																													Main.menuMode = 1;
																												}
																												Main.PlaySound(11, -1, -1, 1);
																											}
																											else
																											{
																												if (this.selectedMenu == 5)
																												{
																													Main.PlaySound(10, -1, -1, 1);
																													Main.menuMode = 16;
																													Main.newWorldName = "World " + (Main.numLoadWorlds + 1);
																												}
																												else
																												{
																													if (this.selectedMenu == 6)
																													{
																														Main.PlaySound(10, -1, -1, 1);
																														Main.menuMode = 8;
																													}
																													else
																													{
																														if (this.selectedMenu >= 0)
																														{
																															if (Main.menuMultiplayer)
																															{
																																Main.PlaySound(10, -1, -1, 1);
																																Main.worldPathName = Main.loadWorldPath[this.selectedMenu];
																																Main.menuMode = 30;
																															}
																															else
																															{
																																Main.PlaySound(10, -1, -1, 1);
																																Main.worldPathName = Main.loadWorldPath[this.selectedMenu];
																																WorldGen.playWorld();
																																Main.menuMode = 10;
																															}
																														}
																													}
																												}
																											}
																										}
																										else
																										{
																											if (Main.menuMode == 7)
																											{
																												string a = Main.newWorldName;
																												Main.newWorldName = Main.GetInputText(Main.newWorldName);
																												if (Main.newWorldName.Length > 20)
																												{
																													Main.newWorldName = Main.newWorldName.Substring(0, 20);
																												}
																												if (a != Main.newWorldName)
																												{
																													Main.PlaySound(12, -1, -1, 1);
																												}
																												array8[0] = "Enter World Name:";
																												array2[2] = true;
																												if (Main.newWorldName != "")
																												{
																													if (Main.newWorldName.Substring(0, 1) == " ")
																													{
																														Main.newWorldName = "";
																													}
																													for (int num12 = 0; num12 < Main.newWorldName.Length; num12++)
																													{
																														if (Main.newWorldName != " ")
																														{
																															array2[2] = false;
																														}
																													}
																												}
																												this.textBlinkerCount++;
																												if (this.textBlinkerCount >= 20)
																												{
																													if (this.textBlinkerState == 0)
																													{
																														this.textBlinkerState = 1;
																													}
																													else
																													{
																														this.textBlinkerState = 0;
																													}
																													this.textBlinkerCount = 0;
																												}
																												array8[1] = Main.newWorldName;
																												if (this.textBlinkerState == 1)
																												{
																													string[] array9;
																													(array9 = array8)[1] = array9[1] + "|";
																													array4[1] = 1;
																												}
																												else
																												{
																													string[] array9;
																													(array9 = array8)[1] = array9[1] + " ";
																												}
																												array[0] = true;
																												array[1] = true;
																												array3[1] = -20;
																												array3[2] = 20;
																												array8[2] = "Accept";
																												array8[3] = "Back";
																												num4 = 4;
																												if (this.selectedMenu == 3)
																												{
																													Main.PlaySound(11, -1, -1, 1);
																													Main.menuMode = 16;
																												}
																												if (this.selectedMenu == 2 || (!array2[2] && Main.inputTextEnter))
																												{
																													Main.menuMode = 10;
																													Main.worldName = Main.newWorldName;
																													Main.worldPathName = Main.nextLoadWorld();
																													WorldGen.CreateNewWorld();
																												}
																											}
																											else
																											{
																												if (Main.menuMode == 8)
																												{
																													num = 220;
																													num3 = 60;
																													array8[5] = "Back";
																													for (int num13 = 0; num13 < 5; num13++)
																													{
																														if (num13 < Main.numLoadWorlds)
																														{
																															array8[num13] = Main.loadWorld[num13];
																														}
																														else
																														{
																															array8[num13] = null;
																														}
																													}
																													num4 = 6;
																													if (this.selectedMenu == 5)
																													{
																														Main.PlaySound(11, -1, -1, 1);
																														Main.menuMode = 1;
																													}
																													else
																													{
																														if (this.selectedMenu >= 0)
																														{
																															this.selectedWorld = this.selectedMenu;
																															Main.PlaySound(10, -1, -1, 1);
																															Main.menuMode = 9;
																														}
																													}
																												}
																												else
																												{
																													if (Main.menuMode == 9)
																													{
																														array8[0] = "Delete " + Main.loadWorld[this.selectedWorld] + "?";
																														array[0] = true;
																														array8[1] = "Yes";
																														array8[2] = "No";
																														num4 = 3;
																														if (this.selectedMenu == 1)
																														{
																															Main.EraseWorld(this.selectedWorld);
																															Main.PlaySound(10, -1, -1, 1);
																															Main.menuMode = 6;
																														}
																														else
																														{
																															if (this.selectedMenu == 2)
																															{
																																Main.PlaySound(11, -1, -1, 1);
																																Main.menuMode = 6;
																															}
																														}
																													}
																													else
																													{
																														if (Main.menuMode == 1111)
																														{
																															num3 = 60;
																															array3[4] = 10;
																															num4 = 8;
																															if (this.graphics.IsFullScreen)
																															{
																																array8[0] = "Go Windowed";
																															}
																															else
																															{
																																array8[0] = "Go Fullscreen";
																															}
																															this.bgScroll = (int)Math.Round((double)((1f - Main.caveParrallax) * 500f));
																															array8[1] = "Resolution";
																															array8[2] = "Parallax";
																															if (Main.fixedTiming)
																															{
																																array8[3] = "Frame Skip Off";
																															}
																															else
																															{
																																array8[3] = "Frame Skip On";
																															}
																															array8[4] = "Back";
																															if (this.selectedMenu == 4)
																															{
																																Main.PlaySound(11, -1, -1, 1);
																																this.SaveSettings();
																																Main.menuMode = 11;
																															}
																															if (this.selectedMenu == 3)
																															{
																																Main.PlaySound(12, -1, -1, 1);
																																if (Main.fixedTiming)
																																{
																																	Main.fixedTiming = false;
																																}
																																else
																																{
																																	Main.fixedTiming = true;
																																}
																															}
																															if (this.selectedMenu == 2)
																															{
																																Main.PlaySound(11, -1, -1, 1);
																																Main.menuMode = 28;
																															}
																															if (this.selectedMenu == 1)
																															{
																																Main.PlaySound(10, -1, -1, 1);
																																Main.menuMode = 111;
																															}
																															if (this.selectedMenu == 0)
																															{
																																this.graphics.ToggleFullScreen();
																															}
																														}
																														else
																														{
																															if (Main.menuMode == 11)
																															{
																																num = 180;
																																num3 = 48;
																																array3[7] = 10;
																																num4 = 8;
																																array8[0] = "Video";
																																array8[1] = "Cursor Color";
																																array8[2] = "Volume";
																																array8[3] = "Controls";
																																if (Main.autoSave)
																																{
																																	array8[4] = "Autosave On";
																																}
																																else
																																{
																																	array8[4] = "Autosave Off";
																																}
																																if (Main.autoPause)
																																{
																																	array8[5] = "Autopause On";
																																}
																																else
																																{
																																	array8[5] = "Autopause Off";
																																}
																																if (Main.showItemText)
																																{
																																	array8[6] = "Pickup Text On";
																																}
																																else
																																{
																																	array8[6] = "Pickup Text Off";
																																}
																																array8[7] = "Back";
																																if (this.selectedMenu == 7)
																																{
																																	Main.PlaySound(11, -1, -1, 1);
																																	this.SaveSettings();
																																	Main.menuMode = 0;
																																}
																																if (this.selectedMenu == 6)
																																{
																																	Main.PlaySound(12, -1, -1, 1);
																																	if (Main.showItemText)
																																	{
																																		Main.showItemText = false;
																																	}
																																	else
																																	{
																																		Main.showItemText = true;
																																	}
																																}
																																if (this.selectedMenu == 5)
																																{
																																	Main.PlaySound(12, -1, -1, 1);
																																	if (Main.autoPause)
																																	{
																																		Main.autoPause = false;
																																	}
																																	else
																																	{
																																		Main.autoPause = true;
																																	}
																																}
																																if (this.selectedMenu == 4)
																																{
																																	Main.PlaySound(12, -1, -1, 1);
																																	if (Main.autoSave)
																																	{
																																		Main.autoSave = false;
																																	}
																																	else
																																	{
																																		Main.autoSave = true;
																																	}
																																}
																																if (this.selectedMenu == 3)
																																{
																																	Main.PlaySound(11, -1, -1, 1);
																																	Main.menuMode = 27;
																																}
																																if (this.selectedMenu == 2)
																																{
																																	Main.PlaySound(11, -1, -1, 1);
																																	Main.menuMode = 26;
																																}
																																if (this.selectedMenu == 1)
																																{
																																	Main.PlaySound(10, -1, -1, 1);
																																	this.selColor = Main.mouseColor;
																																	Main.menuMode = 25;
																																}
																																if (this.selectedMenu == 0)
																																{
																																	Main.PlaySound(10, -1, -1, 1);
																																	Main.menuMode = 1111;
																																}
																															}
																															else
																															{
																																if (Main.menuMode == 111)
																																{
																																	num = 240;
																																	num3 = 60;
																																	num4 = 3;
																																	array8[0] = "Fullscreen Resolution";
																																	array8[1] = this.graphics.PreferredBackBufferWidth + "x" + this.graphics.PreferredBackBufferHeight;
																																	array[0] = true;
																																	array3[2] = 170;
																																	array3[1] = 10;
																																	array8[2] = "Back";
																																	if (this.selectedMenu == 1)
																																	{
																																		Main.PlaySound(12, -1, -1, 1);
																																		int num14 = 0;
																																		for (int num15 = 0; num15 < this.numDisplayModes; num15++)
																																		{
																																			if (this.displayWidth[num15] == this.graphics.PreferredBackBufferWidth && this.displayHeight[num15] == this.graphics.PreferredBackBufferHeight)
																																			{
																																				num14 = num15;
																																				break;
																																			}
																																		}
																																		num14++;
																																		if (num14 >= this.numDisplayModes)
																																		{
																																			num14 = 0;
																																		}
																																		this.graphics.PreferredBackBufferWidth = this.displayWidth[num14];
																																		this.graphics.PreferredBackBufferHeight = this.displayHeight[num14];
																																	}
																																	if (this.selectedMenu == 2)
																																	{
																																		if (this.graphics.IsFullScreen)
																																		{
																																			this.graphics.ApplyChanges();
																																		}
																																		Main.menuMode = 1111;
																																		Main.PlaySound(11, -1, -1, 1);
																																	}
																																}
																																else
																																{
																																	if (Main.menuMode == 25)
																																	{
																																		flag = true;
																																		num9 = 370;
																																		num = 240;
																																		num3 = 60;
																																		Main.mouseColor = this.selColor;
																																		num4 = 3;
																																		array8[0] = "";
																																		array8[1] = "Cursor Color";
																																		array[1] = true;
																																		array3[2] = 170;
																																		array3[1] = 10;
																																		array8[2] = "Back";
																																		if (this.selectedMenu == 2)
																																		{
																																			Main.menuMode = 11;
																																			Main.PlaySound(11, -1, -1, 1);
																																		}
																																	}
																																	else
																																	{
																																		if (Main.menuMode == 26)
																																		{
																																			flag2 = true;
																																			num = 240;
																																			num3 = 60;
																																			num4 = 3;
																																			array8[0] = "";
																																			array8[1] = "Volume";
																																			array[1] = true;
																																			array3[2] = 170;
																																			array3[1] = 10;
																																			array8[2] = "Back";
																																			if (this.selectedMenu == 2)
																																			{
																																				Main.menuMode = 11;
																																				Main.PlaySound(11, -1, -1, 1);
																																			}
																																		}
																																		else
																																		{
																																			if (Main.menuMode == 28)
																																			{
																																				Main.caveParrallax = 1f - (float)this.bgScroll / 500f;
																																				flag3 = true;
																																				num = 240;
																																				num3 = 60;
																																				num4 = 3;
																																				array8[0] = "";
																																				array8[1] = "Parallax";
																																				array[1] = true;
																																				array3[2] = 170;
																																				array3[1] = 10;
																																				array8[2] = "Back";
																																				if (this.selectedMenu == 2)
																																				{
																																					Main.menuMode = 1111;
																																					Main.PlaySound(11, -1, -1, 1);
																																				}
																																			}
																																			else
																																			{
																																				if (Main.menuMode == 27)
																																				{
																																					num = 176;
																																					num3 = 30;
																																					num4 = 13;
																																					string[] array10 = new string[]
																																					{
																																						Main.cUp, 
																																						Main.cDown, 
																																						Main.cLeft, 
																																						Main.cRight, 
																																						Main.cJump, 
																																						Main.cThrowItem, 
																																						Main.cInv, 
																																						Main.cHeal, 
																																						Main.cMana, 
																																						Main.cBuff, 
																																						Main.cHook
																																					};
																																					if (this.setKey >= 0)
																																					{
																																						array10[this.setKey] = "_";
																																					}
																																					array8[0] = "Up             " + array10[0];
																																					array8[1] = "Down          " + array10[1];
																																					array8[2] = "Left            " + array10[2];
																																					array8[3] = "Right          " + array10[3];
																																					array8[4] = "Jump          " + array10[4];
																																					array8[5] = "Throw         " + array10[5];
																																					array8[6] = "Inventory      " + array10[6];
																																					array8[7] = "Quick Heal    " + array10[7];
																																					array8[8] = "Quick Mana   " + array10[8];
																																					array8[9] = "Quick Buff    " + array10[9];
																																					array8[10] = "Grapple        " + array10[10];
																																					for (int num16 = 0; num16 < 11; num16++)
																																					{
																																						array7[num16] = true;
																																						array6[num16] = 0.55f;
																																						array4[num16] = -80;
																																					}
																																					array6[11] = 0.8f;
																																					array6[12] = 0.8f;
																																					array3[11] = 6;
																																					array8[11] = "Reset to Default";
																																					array3[12] = 16;
																																					array8[12] = "Back";
																																					if (this.selectedMenu == 12)
																																					{
																																						Main.menuMode = 11;
																																						Main.PlaySound(11, -1, -1, 1);
																																					}
																																					else
																																					{
																																						if (this.selectedMenu == 11)
																																						{
																																							Main.cUp = "W";
																																							Main.cDown = "S";
																																							Main.cLeft = "A";
																																							Main.cRight = "D";
																																							Main.cJump = "Space";
																																							Main.cThrowItem = "Q";
																																							Main.cInv = "Escape";
																																							Main.cHeal = "H";
																																							Main.cMana = "M";
																																							Main.cBuff = "B";
																																							Main.cHook = "E";
																																							this.setKey = -1;
																																							Main.PlaySound(11, -1, -1, 1);
																																						}
																																						else
																																						{
																																							if (this.selectedMenu >= 0)
																																							{
																																								this.setKey = this.selectedMenu;
																																							}
																																						}
																																					}
																																					if (this.setKey >= 0)
																																					{
																																						Keys[] pressedKeys = Main.keyState.GetPressedKeys();
																																						if (pressedKeys.Length > 0)
																																						{
																																							string text = string.Concat(pressedKeys[0]);
																																							if (this.setKey == 0)
																																							{
																																								Main.cUp = text;
																																							}
																																							if (this.setKey == 1)
																																							{
																																								Main.cDown = text;
																																							}
																																							if (this.setKey == 2)
																																							{
																																								Main.cLeft = text;
																																							}
																																							if (this.setKey == 3)
																																							{
																																								Main.cRight = text;
																																							}
																																							if (this.setKey == 4)
																																							{
																																								Main.cJump = text;
																																							}
																																							if (this.setKey == 5)
																																							{
																																								Main.cThrowItem = text;
																																							}
																																							if (this.setKey == 6)
																																							{
																																								Main.cInv = text;
																																							}
																																							if (this.setKey == 7)
																																							{
																																								Main.cHeal = text;
																																							}
																																							if (this.setKey == 8)
																																							{
																																								Main.cMana = text;
																																							}
																																							if (this.setKey == 9)
																																							{
																																								Main.cBuff = text;
																																							}
																																							if (this.setKey == 10)
																																							{
																																								Main.cHook = text;
																																							}
																																							this.setKey = -1;
																																						}
																																					}
																																				}
																																				else
																																				{
																																					if (Main.menuMode == 12)
																																					{
																																						Main.menuServer = false;
																																						array8[0] = "Join";
																																						array8[1] = "Host & Play";
																																						array8[2] = "Back";
																																						if (this.selectedMenu == 0)
																																						{
																																							Main.LoadPlayers();
																																							Main.menuMultiplayer = true;
																																							Main.PlaySound(10, -1, -1, 1);
																																							Main.menuMode = 1;
																																						}
																																						else
																																						{
																																							if (this.selectedMenu == 1)
																																							{
																																								Main.LoadPlayers();
																																								Main.PlaySound(10, -1, -1, 1);
																																								Main.menuMode = 1;
																																								Main.menuMultiplayer = true;
																																								Main.menuServer = true;
																																							}
																																						}
																																						if (this.selectedMenu == 2)
																																						{
																																							Main.PlaySound(11, -1, -1, 1);
																																							Main.menuMode = 0;
																																						}
																																						num4 = 3;
																																					}
																																					else
																																					{
																																						if (Main.menuMode == 13)
																																						{
																																							string a2 = Main.getIP;
																																							Main.getIP = Main.GetInputText(Main.getIP);
																																							if (a2 != Main.getIP)
																																							{
																																								Main.PlaySound(12, -1, -1, 1);
																																							}
																																							array8[0] = "Enter Server IP Address:";
																																							array2[9] = true;
																																							if (Main.getIP != "")
																																							{
																																								if (Main.getIP.Substring(0, 1) == " ")
																																								{
																																									Main.getIP = "";
																																								}
																																								for (int num17 = 0; num17 < Main.getIP.Length; num17++)
																																								{
																																									if (Main.getIP != " ")
																																									{
																																										array2[9] = false;
																																									}
																																								}
																																							}
																																							this.textBlinkerCount++;
																																							if (this.textBlinkerCount >= 20)
																																							{
																																								if (this.textBlinkerState == 0)
																																								{
																																									this.textBlinkerState = 1;
																																								}
																																								else
																																								{
																																									this.textBlinkerState = 0;
																																								}
																																								this.textBlinkerCount = 0;
																																							}
																																							array8[1] = Main.getIP;
																																							if (this.textBlinkerState == 1)
																																							{
																																								string[] array9;
																																								(array9 = array8)[1] = array9[1] + "|";
																																								array4[1] = 1;
																																							}
																																							else
																																							{
																																								string[] array9;
																																								(array9 = array8)[1] = array9[1] + " ";
																																							}
																																							array[0] = true;
																																							array[1] = true;
																																							array3[9] = 44;
																																							array3[10] = 64;
																																							array8[9] = "Accept";
																																							array8[10] = "Back";
																																							num4 = 11;
																																							num = 180;
																																							num3 = 30;
																																							array3[1] = 19;
																																							for (int num18 = 2; num18 < 9; num18++)
																																							{
																																								int num19 = num18 - 2;
																																								if (Main.recentWorld[num19] != null && Main.recentWorld[num19] != "")
																																								{
																																									array8[num18] = string.Concat(new object[]
																																									{
																																										Main.recentWorld[num19], 
																																										" (", 
																																										Main.recentIP[num19], 
																																										":", 
																																										Main.recentPort[num19], 
																																										")"
																																									});
																																								}
																																								else
																																								{
																																									array8[num18] = "";
																																									array[num18] = true;
																																								}
																																								array6[num18] = 0.6f;
																																								array3[num18] = 40;
																																							}
																																							if (this.selectedMenu >= 2 && this.selectedMenu < 9)
																																							{
																																								Main.autoPass = false;
																																								int num20 = this.selectedMenu - 2;
																																								Netplay.serverPort = Main.recentPort[num20];
																																								Main.getIP = Main.recentIP[num20];
																																								if (Netplay.SetIP(Main.getIP))
																																								{
																																									Main.menuMode = 10;
																																									Netplay.StartClient();
																																								}
																																								else
																																								{
																																									if (Netplay.SetIP2(Main.getIP))
																																									{
																																										Main.menuMode = 10;
																																										Netplay.StartClient();
																																									}
																																								}
																																							}
																																							if (this.selectedMenu == 10)
																																							{
																																								Main.PlaySound(11, -1, -1, 1);
																																								Main.menuMode = 1;
																																							}
																																							if (this.selectedMenu == 9 || (!array2[2] && Main.inputTextEnter))
																																							{
																																								Main.PlaySound(12, -1, -1, 1);
																																								Main.menuMode = 131;
																																							}
																																						}
																																						else
																																						{
																																							if (Main.menuMode == 131)
																																							{
																																								int num21 = 7777;
																																								string a3 = Main.getPort;
																																								Main.getPort = Main.GetInputText(Main.getPort);
																																								if (a3 != Main.getPort)
																																								{
																																									Main.PlaySound(12, -1, -1, 1);
																																								}
																																								array8[0] = "Enter Server Port:";
																																								array2[2] = true;
																																								if (Main.getPort != "")
																																								{
																																									bool flag5 = false;
																																									try
																																									{
																																										num21 = Convert.ToInt32(Main.getPort);
																																										if (num21 > 0 && num21 <= 65535)
																																										{
																																											flag5 = true;
																																										}
																																									}
																																									catch
																																									{
																																									}
																																									if (flag5)
																																									{
																																										array2[2] = false;
																																									}
																																								}
																																								this.textBlinkerCount++;
																																								if (this.textBlinkerCount >= 20)
																																								{
																																									if (this.textBlinkerState == 0)
																																									{
																																										this.textBlinkerState = 1;
																																									}
																																									else
																																									{
																																										this.textBlinkerState = 0;
																																									}
																																									this.textBlinkerCount = 0;
																																								}
																																								array8[1] = Main.getPort;
																																								if (this.textBlinkerState == 1)
																																								{
																																									string[] array9;
																																									(array9 = array8)[1] = array9[1] + "|";
																																									array4[1] = 1;
																																								}
																																								else
																																								{
																																									string[] array9;
																																									(array9 = array8)[1] = array9[1] + " ";
																																								}
																																								array[0] = true;
																																								array[1] = true;
																																								array3[1] = -20;
																																								array3[2] = 20;
																																								array8[2] = "Accept";
																																								array8[3] = "Back";
																																								num4 = 4;
																																								if (this.selectedMenu == 3)
																																								{
																																									Main.PlaySound(11, -1, -1, 1);
																																									Main.menuMode = 1;
																																								}
																																								if (this.selectedMenu == 2 || (!array2[2] && Main.inputTextEnter))
																																								{
																																									Netplay.serverPort = num21;
																																									Main.autoPass = false;
																																									if (Netplay.SetIP(Main.getIP))
																																									{
																																										Main.menuMode = 10;
																																										Netplay.StartClient();
																																									}
																																									else
																																									{
																																										if (Netplay.SetIP2(Main.getIP))
																																										{
																																											Main.menuMode = 10;
																																											Netplay.StartClient();
																																										}
																																									}
																																								}
																																							}
																																							else
																																							{
																																								if (Main.menuMode == 16)
																																								{
																																									num = 200;
																																									num3 = 60;
																																									array3[1] = 30;
																																									array3[2] = 30;
																																									array3[3] = 30;
																																									array3[4] = 70;
																																									array8[0] = "Choose world size:";
																																									array[0] = true;
																																									array8[1] = "Small";
																																									array8[2] = "Medium";
																																									array8[3] = "Large";
																																									array8[4] = "Back";
																																									num4 = 5;
																																									if (this.selectedMenu == 4)
																																									{
																																										Main.menuMode = 6;
																																										Main.PlaySound(11, -1, -1, 1);
																																									}
																																									else
																																									{
																																										if (this.selectedMenu > 0)
																																										{
																																											if (this.selectedMenu == 1)
																																											{
																																												Main.maxTilesX = 4200;
																																												Main.maxTilesY = 1200;
																																											}
																																											else
																																											{
																																												if (this.selectedMenu == 2)
																																												{
																																													Main.maxTilesX = 6300;
																																													Main.maxTilesY = 1800;
																																												}
																																												else
																																												{
																																													Main.maxTilesX = 8400;
																																													Main.maxTilesY = 2400;
																																												}
																																											}
																																											Main.menuMode = 7;
																																											Main.PlaySound(10, -1, -1, 1);
																																											WorldGen.setWorldSize();
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			IL_2F8D:
			if (Main.menuMode != num5)
			{
				num4 = 0;
				for (int num22 = 0; num22 < Main.maxMenuItems; num22++)
				{
					this.menuItemScale[num22] = 0.8f;
				}
			}
			int num23 = this.focusMenu;
			this.selectedMenu = -1;
			this.selectedMenu2 = -1;
			this.focusMenu = -1;
			Vector2 origin;
			for (int num24 = 0; num24 < num4; num24++)
			{
				if (array8[num24] != null)
				{
					if (flag)
					{
						string text2 = "";
						for (int num25 = 0; num25 < 6; num25++)
						{
							int num26 = num9;
							int num27 = 370 + Main.screenWidth / 2 - 400;
							if (num25 == 0)
							{
								text2 = "Red:";
							}
							if (num25 == 1)
							{
								text2 = "Green:";
								num26 += 30;
							}
							if (num25 == 2)
							{
								text2 = "Blue:";
								num26 += 60;
							}
							if (num25 == 3)
							{
								text2 = string.Concat(this.selColor.R);
								num27 += 90;
							}
							if (num25 == 4)
							{
								text2 = string.Concat(this.selColor.G);
								num27 += 90;
								num26 += 30;
							}
							if (num25 == 5)
							{
								text2 = string.Concat(this.selColor.B);
								num27 += 90;
								num26 += 60;
							}
							for (int num28 = 0; num28 < 5; num28++)
							{
								Color color2 = Color.Black;
								if (num28 == 4)
								{
									color2 = color;
                                    color2.R = (byte)((255 + color2.R) / 2);
                                    color2.G = (byte)((255 + color2.R) / 2);
                                    color2.B = (byte)((255 + color2.R) / 2);
								}
								int num29 = 255;
								int num30 = (int)color2.R - (255 - num29);
								if (num30 < 0)
								{
									num30 = 0;
								}
								color2 = new Color((int)((byte)num30), (int)((byte)num30), (int)((byte)num30), (int)((byte)num29));
								int num31 = 0;
								int num32 = 0;
								if (num28 == 0)
								{
									num31 = -2;
								}
								if (num28 == 1)
								{
									num31 = 2;
								}
								if (num28 == 2)
								{
									num32 = -2;
								}
								if (num28 == 3)
								{
									num32 = 2;
								}
								SpriteBatch arg_31B2_0 = this.spriteBatch;
								SpriteFont arg_31B2_1 = Main.fontDeathText;
								string arg_31B2_2 = text2;
								Vector2 arg_31B2_3 = new Vector2((float)(num27 + num31), (float)(num26 + num32));
								Color arg_31B2_4 = color2;
								float arg_31B2_5 = 0f;
								origin = default(Vector2);
								arg_31B2_0.DrawString(arg_31B2_1, arg_31B2_2, arg_31B2_3, arg_31B2_4, arg_31B2_5, origin, 0.5f, SpriteEffects.None, 0f);
							}
						}
						bool flag6 = false;
						for (int num33 = 0; num33 < 2; num33++)
						{
							for (int num34 = 0; num34 < 3; num34++)
							{
								int num35 = num9 + num34 * 30 - 12;
								int num36 = 360 + Main.screenWidth / 2 - 400;
								float scale = 0.9f;
								if (num33 == 0)
								{
									num36 -= 70;
									num35 += 2;
								}
								else
								{
									num36 -= 40;
								}
								text2 = "-";
								if (num33 == 1)
								{
									text2 = "+";
								}
								Vector2 vector3 = new Vector2(24f, 24f);
								int num37 = 142;
								if (Main.mouseState.X > num36 && (float)Main.mouseState.X < (float)num36 + vector3.X && Main.mouseState.Y > num35 + 13 && (float)Main.mouseState.Y < (float)(num35 + 13) + vector3.Y)
								{
									if (this.focusColor != (num33 + 1) * (num34 + 10))
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.focusColor = (num33 + 1) * (num34 + 10);
									flag6 = true;
									num37 = 255;
									if (Main.mouseState.LeftButton == ButtonState.Pressed)
									{
										if (this.colorDelay <= 1)
										{
											if (this.colorDelay == 0)
											{
												this.colorDelay = 40;
											}
											else
											{
												this.colorDelay = 3;
											}
											int num38 = num33;
											if (num33 == 0)
											{
												num38 = -1;
												if (this.selColor.R + this.selColor.G + this.selColor.B < 255)
												{
													num38 = 0;
												}
											}
											if (num34 == 0 && (int)this.selColor.R + num38 >= 0 && (int)this.selColor.R + num38 <= 255)
											{
												this.selColor.R = (byte)((int)this.selColor.R + num38);
											}
											if (num34 == 1 && (int)this.selColor.G + num38 >= 0 && (int)this.selColor.G + num38 <= 255)
											{
												this.selColor.G = (byte)((int)this.selColor.G + num38);
											}
											if (num34 == 2 && (int)this.selColor.B + num38 >= 0 && (int)this.selColor.B + num38 <= 255)
											{
												this.selColor.B = (byte)((int)this.selColor.B + num38);
											}
										}
										this.colorDelay--;
									}
									else
									{
										this.colorDelay = 0;
									}
								}
								for (int num39 = 0; num39 < 5; num39++)
								{
									Color color3 = Color.Black;
									if (num39 == 4)
									{
										color3 = color;
                                        color3.R = (byte)((255 + color3.R) / 2);
                                        color3.G = (byte)((255 + color3.R) / 2);
                                        color3.B = (byte)((255 + color3.R) / 2);
									}
									int num40 = (int)color3.R - (255 - num37);
									if (num40 < 0)
									{
										num40 = 0;
									}
									color3 = new Color((int)((byte)num40), (int)((byte)num40), (int)((byte)num40), (int)((byte)num37));
									int num41 = 0;
									int num42 = 0;
									if (num39 == 0)
									{
										num41 = -2;
									}
									if (num39 == 1)
									{
										num41 = 2;
									}
									if (num39 == 2)
									{
										num42 = -2;
									}
									if (num39 == 3)
									{
										num42 = 2;
									}
									SpriteBatch arg_3521_0 = this.spriteBatch;
									SpriteFont arg_3521_1 = Main.fontDeathText;
									string arg_3521_2 = text2;
									Vector2 arg_3521_3 = new Vector2((float)(num36 + num41), (float)(num35 + num42));
									Color arg_3521_4 = color3;
									float arg_3521_5 = 0f;
									origin = default(Vector2);
									arg_3521_0.DrawString(arg_3521_1, arg_3521_2, arg_3521_3, arg_3521_4, arg_3521_5, origin, scale, SpriteEffects.None, 0f);
								}
							}
						}
						if (!flag6)
						{
							this.focusColor = 0;
							this.colorDelay = 0;
						}
					}
					if (flag3)
					{
						int num43 = 400;
						string text3 = "";
						for (int num44 = 0; num44 < 4; num44++)
						{
							int num45 = num43;
							int num46 = 370 + Main.screenWidth / 2 - 400;
							if (num44 == 0)
							{
								text3 = "Parallax: " + this.bgScroll;
							}
							for (int num47 = 0; num47 < 5; num47++)
							{
								Color color4 = Color.Black;
								if (num47 == 4)
								{
									color4 = color;
                                    color4.R = (byte)((255 + color4.R) / 2);
                                    color4.G = (byte)((255 + color4.R) / 2);
                                    color4.B = (byte)((255 + color4.R) / 2);
								}
								int num48 = 255;
								int num49 = (int)color4.R - (255 - num48);
								if (num49 < 0)
								{
									num49 = 0;
								}
								color4 = new Color((int)((byte)num49), (int)((byte)num49), (int)((byte)num49), (int)((byte)num48));
								int num50 = 0;
								int num51 = 0;
								if (num47 == 0)
								{
									num50 = -2;
								}
								if (num47 == 1)
								{
									num50 = 2;
								}
								if (num47 == 2)
								{
									num51 = -2;
								}
								if (num47 == 3)
								{
									num51 = 2;
								}
								SpriteBatch arg_36A4_0 = this.spriteBatch;
								SpriteFont arg_36A4_1 = Main.fontDeathText;
								string arg_36A4_2 = text3;
								Vector2 arg_36A4_3 = new Vector2((float)(num46 + num50), (float)(num45 + num51));
								Color arg_36A4_4 = color4;
								float arg_36A4_5 = 0f;
								origin = default(Vector2);
								arg_36A4_0.DrawString(arg_36A4_1, arg_36A4_2, arg_36A4_3, arg_36A4_4, arg_36A4_5, origin, 0.5f, SpriteEffects.None, 0f);
							}
						}
						bool flag7 = false;
						for (int num52 = 0; num52 < 2; num52++)
						{
							for (int num53 = 0; num53 < 1; num53++)
							{
								int num54 = num43 + num53 * 30 - 12;
								int num55 = 360 + Main.screenWidth / 2 - 400;
								float scale2 = 0.9f;
								if (num52 == 0)
								{
									num55 -= 70;
									num54 += 2;
								}
								else
								{
									num55 -= 40;
								}
								text3 = "-";
								if (num52 == 1)
								{
									text3 = "+";
								}
								Vector2 vector4 = new Vector2(24f, 24f);
								int num56 = 142;
								if (Main.mouseState.X > num55 && (float)Main.mouseState.X < (float)num55 + vector4.X && Main.mouseState.Y > num54 + 13 && (float)Main.mouseState.Y < (float)(num54 + 13) + vector4.Y)
								{
									if (this.focusColor != (num52 + 1) * (num53 + 10))
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.focusColor = (num52 + 1) * (num53 + 10);
									flag7 = true;
									num56 = 255;
									if (Main.mouseState.LeftButton == ButtonState.Pressed)
									{
										if (this.colorDelay <= 1)
										{
											if (this.colorDelay == 0)
											{
												this.colorDelay = 40;
											}
											else
											{
												this.colorDelay = 3;
											}
											int num57 = num52 == 0 ? -1 : num52;
											if (num53 == 0)
											{
												this.bgScroll += num57;
												if (this.bgScroll > 100)
												{
													this.bgScroll = 100;
												}
												if (this.bgScroll < 0)
												{
													this.bgScroll = 0;
												}
											}
										}
										this.colorDelay--;
									}
									else
									{
										this.colorDelay = 0;
									}
								}
								for (int num58 = 0; num58 < 5; num58++)
								{
									Color color5 = Color.Black;
									if (num58 == 4)
									{
										color5 = color;
                                        color5.R = (byte)((255 + color5.R) / 2);
                                        color5.G = (byte)((255 + color5.R) / 2);
                                        color5.B = (byte)((255 + color5.R) / 2);
									}
									int num59 = (int)color5.R - (255 - num56);
									if (num59 < 0)
									{
										num59 = 0;
									}
									color5 = new Color((int)((byte)num59), (int)((byte)num59), (int)((byte)num59), (int)((byte)num56));
									int num60 = 0;
									int num61 = 0;
									if (num58 == 0)
									{
										num60 = -2;
									}
									if (num58 == 1)
									{
										num60 = 2;
									}
									if (num58 == 2)
									{
										num61 = -2;
									}
									if (num58 == 3)
									{
										num61 = 2;
									}
									SpriteBatch arg_3947_0 = this.spriteBatch;
									SpriteFont arg_3947_1 = Main.fontDeathText;
									string arg_3947_2 = text3;
									Vector2 arg_3947_3 = new Vector2((float)(num55 + num60), (float)(num54 + num61));
									Color arg_3947_4 = color5;
									float arg_3947_5 = 0f;
									origin = default(Vector2);
									arg_3947_0.DrawString(arg_3947_1, arg_3947_2, arg_3947_3, arg_3947_4, arg_3947_5, origin, scale2, SpriteEffects.None, 0f);
								}
							}
						}
						if (!flag7)
						{
							this.focusColor = 0;
							this.colorDelay = 0;
						}
					}
					if (flag2)
					{
						int num62 = 400;
						string text4 = "";
						for (int num63 = 0; num63 < 4; num63++)
						{
							int num64 = num62;
							int num65 = 370 + Main.screenWidth / 2 - 400;
							if (num63 == 0)
							{
								text4 = "Sound:";
							}
							if (num63 == 1)
							{
								text4 = "Music:";
								num64 += 30;
							}
							if (num63 == 2)
							{
								text4 = Math.Round((double)(Main.soundVolume * 100f)) + "%";
								num65 += 90;
							}
							if (num63 == 3)
							{
								text4 = Math.Round((double)(Main.musicVolume * 100f)) + "%";
								num65 += 90;
								num64 += 30;
							}
							for (int num66 = 0; num66 < 5; num66++)
							{
								Color color6 = Color.Black;
								if (num66 == 4)
								{
									color6 = color;
                                    color6.R = (byte)((255 + color6.R) / 2);
                                    color6.G = (byte)((255 + color6.R) / 2);
                                    color6.B = (byte)((255 + color6.R) / 2);
								}
								int num67 = 255;
								int num68 = (int)color6.R - (255 - num67);
								if (num68 < 0)
								{
									num68 = 0;
								}
								color6 = new Color((int)((byte)num68), (int)((byte)num68), (int)((byte)num68), (int)((byte)num67));
								int num69 = 0;
								int num70 = 0;
								if (num66 == 0)
								{
									num69 = -2;
								}
								if (num66 == 1)
								{
									num69 = 2;
								}
								if (num66 == 2)
								{
									num70 = -2;
								}
								if (num66 == 3)
								{
									num70 = 2;
								}
								SpriteBatch arg_3B30_0 = this.spriteBatch;
								SpriteFont arg_3B30_1 = Main.fontDeathText;
								string arg_3B30_2 = text4;
								Vector2 arg_3B30_3 = new Vector2((float)(num65 + num69), (float)(num64 + num70));
								Color arg_3B30_4 = color6;
								float arg_3B30_5 = 0f;
								origin = default(Vector2);
								arg_3B30_0.DrawString(arg_3B30_1, arg_3B30_2, arg_3B30_3, arg_3B30_4, arg_3B30_5, origin, 0.5f, SpriteEffects.None, 0f);
							}
						}
						bool flag8 = false;
						for (int num71 = 0; num71 < 2; num71++)
						{
							for (int num72 = 0; num72 < 2; num72++)
							{
								int num73 = num62 + num72 * 30 - 12;
								int num74 = 360 + Main.screenWidth / 2 - 400;
								float scale3 = 0.9f;
								if (num71 == 0)
								{
									num74 -= 70;
									num73 += 2;
								}
								else
								{
									num74 -= 40;
								}
								text4 = "-";
								if (num71 == 1)
								{
									text4 = "+";
								}
								Vector2 vector5 = new Vector2(24f, 24f);
								int num75 = 142;
								if (Main.mouseState.X > num74 && (float)Main.mouseState.X < (float)num74 + vector5.X && Main.mouseState.Y > num73 + 13 && (float)Main.mouseState.Y < (float)(num73 + 13) + vector5.Y)
								{
									if (this.focusColor != (num71 + 1) * (num72 + 10))
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									this.focusColor = (num71 + 1) * (num72 + 10);
									flag8 = true;
									num75 = 255;
									if (Main.mouseState.LeftButton == ButtonState.Pressed)
									{
										if (this.colorDelay <= 1)
										{
											if (this.colorDelay == 0)
											{
												this.colorDelay = 40;
											}
											else
											{
												this.colorDelay = 3;
											}
											int num76 = num71 == 0 ? -1 : num71;
											if (num72 == 0)
											{
												Main.soundVolume += (float)num76 * 0.01f;
												if (Main.soundVolume > 1f)
												{
													Main.soundVolume = 1f;
												}
												if (Main.soundVolume < 0f)
												{
													Main.soundVolume = 0f;
												}
											}
											if (num72 == 1)
											{
												Main.musicVolume += (float)num76 * 0.01f;
												if (Main.musicVolume > 1f)
												{
													Main.musicVolume = 1f;
												}
												if (Main.musicVolume < 0f)
												{
													Main.musicVolume = 0f;
												}
											}
										}
										this.colorDelay--;
									}
									else
									{
										this.colorDelay = 0;
									}
								}
								for (int num77 = 0; num77 < 5; num77++)
								{
									Color color7 = Color.Black;
									if (num77 == 4)
									{
										color7 = color;
                                        color7.R = (byte)((255 + color7.R) / 2);
                                        color7.G = (byte)((255 + color7.R) / 2);
                                        color7.B = (byte)((255 + color7.R) / 2);
									}
									int num78 = (int)color7.R - (255 - num75);
									if (num78 < 0)
									{
										num78 = 0;
									}
									color7 = new Color((int)((byte)num78), (int)((byte)num78), (int)((byte)num78), (int)((byte)num75));
									int num79 = 0;
									int num80 = 0;
									if (num77 == 0)
									{
										num79 = -2;
									}
									if (num77 == 1)
									{
										num79 = 2;
									}
									if (num77 == 2)
									{
										num80 = -2;
									}
									if (num77 == 3)
									{
										num80 = 2;
									}
									SpriteBatch arg_3E2D_0 = this.spriteBatch;
									SpriteFont arg_3E2D_1 = Main.fontDeathText;
									string arg_3E2D_2 = text4;
									Vector2 arg_3E2D_3 = new Vector2((float)(num74 + num79), (float)(num73 + num80));
									Color arg_3E2D_4 = color7;
									float arg_3E2D_5 = 0f;
									origin = default(Vector2);
									arg_3E2D_0.DrawString(arg_3E2D_1, arg_3E2D_2, arg_3E2D_3, arg_3E2D_4, arg_3E2D_5, origin, scale3, SpriteEffects.None, 0f);
								}
							}
						}
						if (!flag8)
						{
							this.focusColor = 0;
							this.colorDelay = 0;
						}
					}
					for (int num81 = 0; num81 < 5; num81++)
					{
						Color color8 = Color.Black;
						if (num81 == 4)
						{
							color8 = color;
							if (array5[num24] == 2)
							{
								color8 = Main.hcColor;
							}
							else
							{
								if (array5[num24] == 1)
								{
									color8 = Main.mcColor;
								}
							}
                            color8.R = (byte)((255 + color8.R) / 2);
                            color8.G = (byte)((255 + color8.G) / 2);
                            color8.B = (byte)((255 + color8.B) / 2);
						}
						int num82 = (int)(255f * (this.menuItemScale[num24] * 2f - 1f));
						if (array[num24])
						{
							num82 = 255;
						}
						int num83 = (int)color8.R - (255 - num82);
						if (num83 < 0)
						{
							num83 = 0;
						}
						int num84 = (int)color8.G - (255 - num82);
						if (num84 < 0)
						{
							num84 = 0;
						}
						int num85 = (int)color8.B - (255 - num82);
						if (num85 < 0)
						{
							num85 = 0;
						}
						color8 = new Color((int)((byte)num83), (int)((byte)num84), (int)((byte)num85), (int)((byte)num82));
						int num86 = 0;
						int num87 = 0;
						if (num81 == 0)
						{
							num86 = -2;
						}
						if (num81 == 1)
						{
							num86 = 2;
						}
						if (num81 == 2)
						{
							num87 = -2;
						}
						if (num81 == 3)
						{
							num87 = 2;
						}
						Vector2 origin2 = Main.fontDeathText.MeasureString(array8[num24]);
						origin2.X *= 0.5f;
						origin2.Y *= 0.5f;
						float num88 = this.menuItemScale[num24];
						if (Main.menuMode == 15 && num24 == 0)
						{
							num88 *= 0.35f;
						}
						else
						{
							if (Main.netMode == 2)
							{
								num88 *= 0.5f;
							}
						}
						num88 *= array6[num24];
						if (!array7[num24])
						{
							this.spriteBatch.DrawString(Main.fontDeathText, array8[num24], new Vector2((float)(num2 + num86 + array4[num24]), (float)(num + num3 * num24 + num87) + origin2.Y * array6[num24] + (float)array3[num24]), color8, 0f, origin2, num88, SpriteEffects.None, 0f);
						}
						else
						{
							this.spriteBatch.DrawString(Main.fontDeathText, array8[num24], new Vector2((float)(num2 + num86 + array4[num24]), (float)(num + num3 * num24 + num87) + origin2.Y * array6[num24] + (float)array3[num24]), color8, 0f, new Vector2(0f, origin2.Y), num88, SpriteEffects.None, 0f);
						}
					}
					if (!array7[num24])
					{
						if ((float)Main.mouseState.X > (float)num2 - (float)(array8[num24].Length * 10) * array6[num24] + (float)array4[num24] && (float)Main.mouseState.X < (float)num2 + (float)(array8[num24].Length * 10) * array6[num24] + (float)array4[num24] && Main.mouseState.Y > num + num3 * num24 + array3[num24] && (float)Main.mouseState.Y < (float)(num + num3 * num24 + array3[num24]) + 50f * array6[num24] && Main.hasFocus)
						{
							this.focusMenu = num24;
							if (array[num24] || array2[num24])
							{
								this.focusMenu = -1;
							}
							else
							{
								if (num23 != this.focusMenu)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									this.selectedMenu = num24;
								}
								if (Main.mouseRightRelease && Main.mouseState.RightButton == ButtonState.Pressed)
								{
									this.selectedMenu2 = num24;
								}
							}
						}
					}
					else
					{
						if (Main.mouseState.X > num2 + array4[num24] && (float)Main.mouseState.X < (float)num2 + (float)(array8[num24].Length * 20) * array6[num24] + (float)array4[num24] && Main.mouseState.Y > num + num3 * num24 + array3[num24] && (float)Main.mouseState.Y < (float)(num + num3 * num24 + array3[num24]) + 50f * array6[num24] && Main.hasFocus)
						{
							this.focusMenu = num24;
							if (array[num24] || array2[num24])
							{
								this.focusMenu = -1;
							}
							else
							{
								if (num23 != this.focusMenu)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
								{
									this.selectedMenu = num24;
								}
								if (Main.mouseRightRelease && Main.mouseState.RightButton == ButtonState.Pressed)
								{
									this.selectedMenu2 = num24;
								}
							}
						}
					}
				}
			}
			for (int num89 = 0; num89 < Main.maxMenuItems; num89++)
			{
				if (num89 == this.focusMenu)
				{
					if (this.menuItemScale[num89] < 1f)
					{
						this.menuItemScale[num89] += 0.02f;
					}
					if (this.menuItemScale[num89] > 1f)
					{
						this.menuItemScale[num89] = 1f;
					}
				}
				else
				{
					if ((double)this.menuItemScale[num89] > 0.8)
					{
						this.menuItemScale[num89] -= 0.02f;
					}
				}
			}
			if (num6 >= 0)
			{
				Main.loadPlayer[num6].PlayerFrame();
				Main.loadPlayer[num6].position.X = (float)num7 + Main.screenPosition.X;
				Main.loadPlayer[num6].position.Y = (float)num8 + Main.screenPosition.Y;
				this.DrawPlayer(Main.loadPlayer[num6]);
			}
			for (int num90 = 0; num90 < 5; num90++)
			{
				Color color9 = Color.Black;
				if (num90 == 4)
				{
					color9 = color;
                    color9.R = (byte)((255 + color9.R) / 2);
                    color9.G = (byte)((255 + color9.R) / 2);
                    color9.B = (byte)((255 + color9.R) / 2);
				}
				color9.A = (byte)((float)color9.A * 0.3f);
				int num91 = 0;
				int num92 = 0;
				if (num90 == 0)
				{
					num91 = -2;
				}
				if (num90 == 1)
				{
					num91 = 2;
				}
				if (num90 == 2)
				{
					num92 = -2;
				}
				if (num90 == 3)
				{
					num92 = 2;
				}
				string text5 = "Copyright © 2014 DartPower Team";
				Vector2 origin3 = Main.fontMouseText.MeasureString(text5);
				origin3.X *= 0.5f;
				origin3.Y *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, text5, new Vector2((float)Main.screenWidth - origin3.X + (float)num91 - 10f, (float)Main.screenHeight - origin3.Y + (float)num92 - 2f), color9, 0f, origin3, 1f, SpriteEffects.None, 0f);
			}
			for (int num93 = 0; num93 < 5; num93++)
			{
				Color color10 = Color.Black;
				if (num93 == 4)
				{
					color10 = color;
                    color10.R = (byte)((255 + color10.R) / 2);
                    color10.G = (byte)((255 + color10.R) / 2);
                    color10.B = (byte)((255 + color10.R) / 2);
				}
				color10.A = (byte)((float)color10.A * 0.3f);
				int num94 = 0;
				int num95 = 0;
				if (num93 == 0)
				{
					num94 = -2;
				}
				if (num93 == 1)
				{
					num94 = 2;
				}
				if (num93 == 2)
				{
					num95 = -2;
				}
				if (num93 == 3)
				{
					num95 = 2;
				}
				Vector2 origin4 = Main.fontMouseText.MeasureString(Main.versionNumber);
				origin4.X *= 0.5f;
				origin4.Y *= 0.5f;
				this.spriteBatch.DrawString(Main.fontMouseText, Main.versionNumber, new Vector2(origin4.X + (float)num94 + 10f, (float)Main.screenHeight - origin4.Y + (float)num95 - 2f), color10, 0f, origin4, 1f, SpriteEffects.None, 0f);
			}
			SpriteBatch arg_4752_0 = this.spriteBatch;
			Texture2D arg_4752_1 = Main.cursorTexture;
			Vector2 arg_4752_2 = new Vector2((float)(Main.mouseState.X + 1), (float)(Main.mouseState.Y + 1));
			Rectangle? arg_4752_3 = new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height));
			Color arg_4752_4 = new Color((int)((float)Main.cursorColor.R * 0.2f), (int)((float)Main.cursorColor.G * 0.2f), (int)((float)Main.cursorColor.B * 0.2f), (int)((float)Main.cursorColor.A * 0.5f));
			float arg_4752_5 = 0f;
			origin = default(Vector2);
			arg_4752_0.Draw(arg_4752_1, arg_4752_2, arg_4752_3, arg_4752_4, arg_4752_5, origin, Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
			SpriteBatch arg_47BC_0 = this.spriteBatch;
			Texture2D arg_47BC_1 = Main.cursorTexture;
			Vector2 arg_47BC_2 = new Vector2((float)Main.mouseState.X, (float)Main.mouseState.Y);
			Rectangle? arg_47BC_3 = new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height));
			Color arg_47BC_4 = Main.cursorColor;
			float arg_47BC_5 = 0f;
			origin = default(Vector2);
			arg_47BC_0.Draw(arg_47BC_1, arg_47BC_2, arg_47BC_3, arg_47BC_4, arg_47BC_5, origin, Main.cursorScale, SpriteEffects.None, 0f);
			if (Main.fadeCounter > 0)
			{
				Color white = Color.White;
				Main.fadeCounter--;
				float num96 = (float)Main.fadeCounter / 75f * 255f;
				byte b2 = (byte)num96;
				white = new Color((int)b2, (int)b2, (int)b2, (int)b2);
				this.spriteBatch.Draw(Main.fadeTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), white);
			}
			this.spriteBatch.End();
			if (Main.mouseState.LeftButton == ButtonState.Pressed)
			{
				Main.mouseLeftRelease = false;
			}
			else
			{
				Main.mouseLeftRelease = true;
			}
			if (Main.mouseState.RightButton == ButtonState.Pressed)
			{
				Main.mouseRightRelease = false;
				return;
			}
			Main.mouseRightRelease = true;
		}
		public static void CursorColor()
		{
			Main.cursorAlpha += (float)Main.cursorColorDirection * 0.015f;
			if (Main.cursorAlpha >= 1f)
			{
				Main.cursorAlpha = 1f;
				Main.cursorColorDirection = -1;
			}
			if ((double)Main.cursorAlpha <= 0.6)
			{
				Main.cursorAlpha = 0.6f;
				Main.cursorColorDirection = 1;
			}
			float num = Main.cursorAlpha * 0.3f + 0.7f;
			byte r = (byte)((float)Main.mouseColor.R * Main.cursorAlpha);
			byte g = (byte)((float)Main.mouseColor.G * Main.cursorAlpha);
			byte b = (byte)((float)Main.mouseColor.B * Main.cursorAlpha);
			byte a = (byte)(255f * num);
			Main.cursorColor = new Color((int)r, (int)g, (int)b, (int)a);
			Main.cursorScale = Main.cursorAlpha * 0.3f + 0.7f + 0.1f;
		}
		protected void DrawSplash(GameTime gameTime)
		{
			base.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
			this.spriteBatch.Begin();
			this.splashCounter++;
			Color white = Color.White;
			byte b = 0;
			if (this.splashCounter <= 75)
			{
				float num = (float)this.splashCounter / 75f * 255f;
				b = (byte)num;
			}
			else
			{
				if (this.splashCounter <= 200)
				{
					b = 255;
				}
				else
				{
					if (this.splashCounter <= 275)
					{
						int num2 = 275 - this.splashCounter;
						float num3 = (float)num2 / 75f * 255f;
						b = (byte)num3;
					}
					else
					{
						Main.showSplash = false;
						Main.fadeCounter = 75;
					}
				}
			}
			white = new Color((int)b, (int)b, (int)b, (int)b);
			this.spriteBatch.Draw(Main.splashTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), white);
			this.spriteBatch.End();
		}
		protected override void Draw(GameTime gameTime)
		{
			if (!Main.dedServ)
			{
				int arg_23_0 = Main.screenWidth;
				Viewport viewport = base.GraphicsDevice.Viewport;
				if (arg_23_0 == viewport.Width)
				{
					int arg_3E_0 = Main.screenHeight;
					viewport = base.GraphicsDevice.Viewport;
					if (arg_3E_0 == viewport.Height)
					{
						goto IL_4D;
					}
				}
				if (Main.gamePaused)
				{
					Lighting.resize = true;
				}
				IL_4D:
				viewport = base.GraphicsDevice.Viewport;
				Main.screenWidth = viewport.Width;
				viewport = base.GraphicsDevice.Viewport;
				Main.screenHeight = viewport.Height;
				bool flag = false;
				if (Main.screenWidth > Main.maxScreenW)
				{
					Main.screenWidth = Main.maxScreenW;
					flag = true;
				}
				if (Main.screenHeight > Main.maxScreenH)
				{
					Main.screenHeight = Main.maxScreenH;
					flag = true;
				}
				if (Main.screenWidth < Main.minScreenW)
				{
					Main.screenWidth = Main.minScreenW;
					flag = true;
				}
				if (Main.screenHeight < Main.minScreenH)
				{
					Main.screenHeight = Main.minScreenH;
					flag = true;
				}
				if (flag)
				{
					this.graphics.PreferredBackBufferWidth = Main.screenWidth;
					this.graphics.PreferredBackBufferHeight = Main.screenHeight;
					this.graphics.ApplyChanges();
				}
			}
			Main.CursorColor();
			Main.drawTime++;
			Main.screenLastPosition = Main.screenPosition;
			if (Main.stackSplit == 0)
			{
				Main.stackCounter = 0;
				Main.stackDelay = 7;
			}
			else
			{
				Main.stackCounter++;
				if (Main.stackCounter >= 30)
				{
					Main.stackDelay--;
					if (Main.stackDelay < 2)
					{
						Main.stackDelay = 2;
					}
					Main.stackCounter = 0;
				}
			}
			Main.mouseTextColor += (byte)Main.mouseTextColorChange;
			if (Main.mouseTextColor >= 250)
			{
				Main.mouseTextColorChange = -4;
			}
			if (Main.mouseTextColor <= 175)
			{
				Main.mouseTextColorChange = 4;
			}
			if (Main.myPlayer >= 0)
			{
				Main.player[Main.myPlayer].mouseInterface = false;
			}
			Main.toolTip = new Item();
			if (!Main.gameMenu && Main.netMode != 2)
			{
				int num = Main.mouseState.X;
				int num2 = Main.mouseState.Y;
				if (num < 0)
				{
					num = 0;
				}
				if (num > Main.screenWidth)
				{
					num = Main.screenWidth;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > Main.screenHeight)
				{
					num2 = Main.screenHeight;
				}
				Main.screenPosition.X = Main.player[Main.myPlayer].position.X + (float)Main.player[Main.myPlayer].width * 0.5f - (float)Main.screenWidth * 0.5f;
				Main.screenPosition.Y = Main.player[Main.myPlayer].position.Y + (float)Main.player[Main.myPlayer].height * 0.5f - (float)Main.screenHeight * 0.5f;
				Main.screenPosition.X = (float)((int)Main.screenPosition.X);
				Main.screenPosition.Y = (float)((int)Main.screenPosition.Y);
			}
			if (!Main.gameMenu && Main.netMode != 2)
			{
				if (Main.screenPosition.X < Main.leftWorld + 336f + 16f)
				{
					Main.screenPosition.X = Main.leftWorld + 336f + 16f;
				}
				else
				{
					if (Main.screenPosition.X + (float)Main.screenWidth > Main.rightWorld - 336f - 32f)
					{
						Main.screenPosition.X = Main.rightWorld - (float)Main.screenWidth - 336f - 32f;
					}
				}
				if (Main.screenPosition.Y < Main.topWorld + 336f + 16f)
				{
					Main.screenPosition.Y = Main.topWorld + 336f + 16f;
				}
				else
				{
					if (Main.screenPosition.Y + (float)Main.screenHeight > Main.bottomWorld - 336f - 32f)
					{
						Main.screenPosition.Y = Main.bottomWorld - (float)Main.screenHeight - 336f - 32f;
					}
				}
			}
			if (Main.showSplash)
			{
				this.DrawSplash(gameTime);
				return;
			}
			base.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
			this.spriteBatch.Begin();
			double num3 = 0.5;
			int num4 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[Main.background]) - (double)(Main.backgroundWidth[Main.background] / 2));
			int num5 = Main.screenWidth / Main.backgroundWidth[Main.background] + 2;
			int num6 = 0;
			int num7 = 0;
			int num8 = (int)((double)(-(double)Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
			if (Main.gameMenu || Main.netMode == 2)
			{
				num8 = -200;
			}
			Color white = Color.White;
			int num9 = (int)(Main.time / 54000.0 * (double)(Main.screenWidth + Main.sunTexture.Width * 2)) - Main.sunTexture.Width;
			int num10 = 0;
			Color white2 = Color.White;
			float num11 = 1f;
			float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;
			int num12 = (int)(Main.time / 32400.0 * (double)(Main.screenWidth + Main.moonTexture.Width * 2)) - Main.moonTexture.Width;
			int num13 = 0;
			Color white3 = Color.White;
			float num14 = 1f;
			float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
			if (Main.dayTime)
			{
				double num15;
				if (Main.time < 27000.0)
				{
					num15 = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
					num10 = (int)((double)num8 + num15 * 250.0 + 180.0);
				}
				else
				{
					num15 = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
					num10 = (int)((double)num8 + num15 * 250.0 + 180.0);
				}
				num11 = (float)(1.2 - num15 * 0.4);
			}
			else
			{
				double num16;
				if (Main.time < 16200.0)
				{
					num16 = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
					num13 = (int)((double)num8 + num16 * 250.0 + 180.0);
				}
				else
				{
					num16 = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
					num13 = (int)((double)num8 + num16 * 250.0 + 180.0);
				}
				num14 = (float)(1.2 - num16 * 0.4);
			}
			if (Main.dayTime)
			{
				if (Main.time < 13500.0)
				{
					float num17 = (float)(Main.time / 13500.0);
					white2.R = (byte)(num17 * 200f + 55f);
					white2.G = (byte)(num17 * 180f + 75f);
					white2.B = (byte)(num17 * 250f + 5f);
					white.R = (byte)(num17 * 200f + 55f);
					white.G = (byte)(num17 * 200f + 55f);
					white.B = (byte)(num17 * 200f + 55f);
				}
				if (Main.time > 45900.0)
				{
					float num17 = (float)(1.0 - (Main.time / 54000.0 - 0.85) * 6.666666666666667);
					white2.R = (byte)(num17 * 120f + 55f);
					white2.G = (byte)(num17 * 100f + 25f);
					white2.B = (byte)(num17 * 120f + 55f);
					white.R = (byte)(num17 * 200f + 55f);
					white.G = (byte)(num17 * 85f + 55f);
					white.B = (byte)(num17 * 135f + 55f);
				}
				else
				{
					if (Main.time > 37800.0)
					{
						float num17 = (float)(1.0 - (Main.time / 54000.0 - 0.7) * 6.666666666666667);
						white2.R = (byte)(num17 * 80f + 175f);
						white2.G = (byte)(num17 * 130f + 125f);
						white2.B = (byte)(num17 * 100f + 155f);
						white.R = (byte)(num17 * 0f + 255f);
						white.G = (byte)(num17 * 115f + 140f);
						white.B = (byte)(num17 * 75f + 180f);
					}
				}
			}
			if (!Main.dayTime)
			{
				if (Main.bloodMoon)
				{
					if (Main.time < 16200.0)
					{
						float num17 = (float)(1.0 - Main.time / 16200.0);
						white3.R = (byte)(num17 * 10f + 205f);
						white3.G = (byte)(num17 * 170f + 55f);
						white3.B = (byte)(num17 * 200f + 55f);
						white.R = (byte)(60f - num17 * 60f + 55f);
						white.G = (byte)(num17 * 40f + 15f);
						white.B = (byte)(num17 * 40f + 15f);
					}
					else
					{
						if (Main.time >= 16200.0)
						{
							float num17 = (float)((Main.time / 32400.0 - 0.5) * 2.0);
							white3.R = (byte)(num17 * 50f + 205f);
							white3.G = (byte)(num17 * 100f + 155f);
							white3.B = (byte)(num17 * 100f + 155f);
							white3.R = (byte)(num17 * 10f + 205f);
							white3.G = (byte)(num17 * 170f + 55f);
							white3.B = (byte)(num17 * 200f + 55f);
							white.R = (byte)(60f - num17 * 60f + 55f);
							white.G = (byte)(num17 * 40f + 15f);
							white.B = (byte)(num17 * 40f + 15f);
						}
					}
				}
				else
				{
					if (Main.time < 16200.0)
					{
						float num17 = (float)(1.0 - Main.time / 16200.0);
						white3.R = (byte)(num17 * 10f + 205f);
						white3.G = (byte)(num17 * 70f + 155f);
						white3.B = (byte)(num17 * 100f + 155f);
						white.R = (byte)(num17 * 40f + 15f);
						white.G = (byte)(num17 * 40f + 15f);
						white.B = (byte)(num17 * 40f + 15f);
					}
					else
					{
						if (Main.time >= 16200.0)
						{
							float num17 = (float)((Main.time / 32400.0 - 0.5) * 2.0);
							white3.R = (byte)(num17 * 50f + 205f);
							white3.G = (byte)(num17 * 100f + 155f);
							white3.B = (byte)(num17 * 100f + 155f);
							white.R = (byte)(num17 * 40f + 15f);
							white.G = (byte)(num17 * 40f + 15f);
							white.B = (byte)(num17 * 40f + 15f);
						}
					}
				}
			}
			if (Main.gameMenu || Main.netMode == 2)
			{
				num8 = 0;
				if (!Main.dayTime)
				{
					white.R = 55;
					white.G = 55;
					white.B = 55;
				}
			}
			if (Main.evilTiles > 0)
			{
				float num18 = (float)Main.evilTiles / 500f;
				if (num18 > 1f)
				{
					num18 = 1f;
				}
				int num19 = (int)white.R;
				int num20 = (int)white.G;
				int num21 = (int)white.B;
				num19 += (int)(10f * num18);
				num20 -= (int)(90f * num18 * ((float)white.G / 255f));
				num21 -= (int)(190f * num18 * ((float)white.B / 255f));
				if (num19 > 255)
				{
					num19 = 255;
				}
				if (num20 < 15)
				{
					num20 = 15;
				}
				if (num21 < 15)
				{
					num21 = 15;
				}
				white.R = (byte)num19;
				white.G = (byte)num20;
				white.B = (byte)num21;
				num19 = (int)white2.R;
				num20 = (int)white2.G;
				num21 = (int)white2.B;
				num19 -= (int)(100f * num18 * ((float)white2.R / 255f));
				num20 -= (int)(160f * num18 * ((float)white2.G / 255f));
				num21 -= (int)(170f * num18 * ((float)white2.B / 255f));
				if (num19 < 15)
				{
					num19 = 15;
				}
				if (num20 < 15)
				{
					num20 = 15;
				}
				if (num21 < 15)
				{
					num21 = 15;
				}
				white2.R = (byte)num19;
				white2.G = (byte)num20;
				white2.B = (byte)num21;
				num19 = (int)white3.R;
				num20 = (int)white3.G;
				num21 = (int)white3.B;
				num19 -= (int)(140f * num18 * ((float)white3.R / 255f));
				num20 -= (int)(170f * num18 * ((float)white3.G / 255f));
				num21 -= (int)(190f * num18 * ((float)white3.B / 255f));
				if (num19 < 15)
				{
					num19 = 15;
				}
				if (num20 < 15)
				{
					num20 = 15;
				}
				if (num21 < 15)
				{
					num21 = 15;
				}
				white3.R = (byte)num19;
				white3.G = (byte)num20;
				white3.B = (byte)num21;
			}
			if (Main.jungleTiles > 0)
			{
				float num22 = (float)Main.jungleTiles / 200f;
				if (num22 > 1f)
				{
					num22 = 1f;
				}
				int num23 = (int)white.R;
				int num24 = (int)white.G;
				int num25 = (int)white.B;
				num23 -= (int)(20f * num22 * ((float)white.R / 255f));
				num25 -= (int)(120f * num22 * ((float)white.B / 255f));
				if (num24 > 255)
				{
					num24 = 255;
				}
				if (num24 < 15)
				{
					num24 = 15;
				}
				if (num23 > 255)
				{
					num23 = 255;
				}
				if (num23 < 15)
				{
					num23 = 15;
				}
				if (num25 < 15)
				{
					num25 = 15;
				}
				white.R = (byte)num23;
				white.G = (byte)num24;
				white.B = (byte)num25;
				num23 = (int)white2.R;
				num24 = (int)white2.G;
				num25 = (int)white2.B;
				num24 -= (int)(10f * num22 * ((float)white2.G / 255f));
				num23 -= (int)(50f * num22 * ((float)white2.R / 255f));
				num25 -= (int)(10f * num22 * ((float)white2.B / 255f));
				if (num23 < 15)
				{
					num23 = 15;
				}
				if (num24 < 15)
				{
					num24 = 15;
				}
				if (num25 < 15)
				{
					num25 = 15;
				}
				white2.R = (byte)num23;
				white2.G = (byte)num24;
				white2.B = (byte)num25;
				num23 = (int)white3.R;
				num24 = (int)white3.G;
				num25 = (int)white3.B;
				num24 -= (int)(140f * num22 * ((float)white3.R / 255f));
				num23 -= (int)(170f * num22 * ((float)white3.G / 255f));
				num25 -= (int)(190f * num22 * ((float)white3.B / 255f));
				if (num23 < 15)
				{
					num23 = 15;
				}
				if (num24 < 15)
				{
					num24 = 15;
				}
				if (num25 < 15)
				{
					num25 = 15;
				}
				white3.R = (byte)num23;
				white3.G = (byte)num24;
				white3.B = (byte)num25;
			}
			Main.tileColor.A = 255;
            Main.tileColor.R = (byte)((white.R + white.B + white.G) / 3);
            Main.tileColor.G = (byte)((white.R + white.B + white.G) / 3);
            Main.tileColor.B = (byte)((white.R + white.B + white.G) / 3);
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int i = 0; i < num5; i++)
				{
					this.spriteBatch.Draw(Main.backgroundTexture[Main.background], new Rectangle(num4 + Main.backgroundWidth[Main.background] * i, num8, Main.backgroundWidth[Main.background], Main.backgroundHeight[Main.background]), white);
				}
			}
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0 && 255 - Main.tileColor.R - 100 > 0 && Main.netMode != 2)
			{
				for (int j = 0; j < Main.numStars; j++)
				{
					Color color = default(Color);
					float num26 = (float)Main.evilTiles / 500f;
					if (num26 > 1f)
					{
						num26 = 1f;
					}
					num26 = 1f - num26 * 0.5f;
					if (Main.evilTiles <= 0)
					{
						num26 = 1f;
					}
					int num27 = (int)((float)(255 - Main.tileColor.R - 100) * Main.star[j].twinkle * num26);
					int num28 = (int)((float)(255 - Main.tileColor.G - 100) * Main.star[j].twinkle * num26);
					int num29 = (int)((float)(255 - Main.tileColor.B - 100) * Main.star[j].twinkle * num26);
					if (num27 < 0)
					{
						num27 = 0;
					}
					if (num28 < 0)
					{
						num28 = 0;
					}
					if (num29 < 0)
					{
						num29 = 0;
					}
					color.R = (byte)num27;
					color.G = (byte)((float)num28 * num26);
					color.B = (byte)((float)num29 * num26);
					float num30 = Main.star[j].position.X * ((float)Main.screenWidth / 800f);
					float num31 = Main.star[j].position.Y * ((float)Main.screenHeight / 600f);
					this.spriteBatch.Draw(Main.starTexture[Main.star[j].type], new Vector2(num30 + (float)Main.starTexture[Main.star[j].type].Width * 0.5f, num31 + (float)Main.starTexture[Main.star[j].type].Height * 0.5f + (float)num8), new Rectangle?(new Rectangle(0, 0, Main.starTexture[Main.star[j].type].Width, Main.starTexture[Main.star[j].type].Height)), color, Main.star[j].rotation, new Vector2((float)Main.starTexture[Main.star[j].type].Width * 0.5f, (float)Main.starTexture[Main.star[j].type].Height * 0.5f), Main.star[j].scale * Main.star[j].twinkle, SpriteEffects.None, 0f);
				}
			}
			if (Main.dayTime)
			{
				if (!Main.gameMenu && Main.player[Main.myPlayer].head == 12)
				{
					this.spriteBatch.Draw(Main.sun2Texture, new Vector2((float)num9, (float)(num10 + (int)Main.sunModY)), new Rectangle?(new Rectangle(0, 0, Main.sunTexture.Width, Main.sunTexture.Height)), white2, rotation, new Vector2((float)(Main.sunTexture.Width / 2), (float)(Main.sunTexture.Height / 2)), num11, SpriteEffects.None, 0f);
				}
				else
				{
					this.spriteBatch.Draw(Main.sunTexture, new Vector2((float)num9, (float)(num10 + (int)Main.sunModY)), new Rectangle?(new Rectangle(0, 0, Main.sunTexture.Width, Main.sunTexture.Height)), white2, rotation, new Vector2((float)(Main.sunTexture.Width / 2), (float)(Main.sunTexture.Height / 2)), num11, SpriteEffects.None, 0f);
				}
			}
			if (!Main.dayTime)
			{
				this.spriteBatch.Draw(Main.moonTexture, new Vector2((float)num12, (float)(num13 + (int)Main.moonModY)), new Rectangle?(new Rectangle(0, Main.moonTexture.Width * Main.moonPhase, Main.moonTexture.Width, Main.moonTexture.Width)), white3, rotation2, new Vector2((float)(Main.moonTexture.Width / 2), (float)(Main.moonTexture.Width / 2)), num14, SpriteEffects.None, 0f);
			}
			Rectangle value;
			if (Main.dayTime)
			{
				value = new Rectangle((int)((double)num9 - (double)Main.sunTexture.Width * 0.5 * (double)num11), (int)((double)num10 - (double)Main.sunTexture.Height * 0.5 * (double)num11 + (double)Main.sunModY), (int)((float)Main.sunTexture.Width * num11), (int)((float)Main.sunTexture.Width * num11));
			}
			else
			{
				value = new Rectangle((int)((double)num12 - (double)Main.moonTexture.Width * 0.5 * (double)num14), (int)((double)num13 - (double)Main.moonTexture.Width * 0.5 * (double)num14 + (double)Main.moonModY), (int)((float)Main.moonTexture.Width * num14), (int)((float)Main.moonTexture.Width * num14));
			}
			Rectangle rectangle = new Rectangle(Main.mouseState.X, Main.mouseState.Y, 1, 1);
			Main.sunModY = (short)((double)Main.sunModY * 0.999);
			Main.moonModY = (short)((double)Main.moonModY * 0.999);
			if (Main.gameMenu && Main.netMode != 1)
			{
				if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.hasFocus)
				{
					if (rectangle.Intersects(value) || Main.grabSky)
					{
						if (Main.dayTime)
						{
							Main.time = 54000.0 * (double)((float)(Main.mouseState.X + Main.sunTexture.Width) / ((float)Main.screenWidth + (float)(Main.sunTexture.Width * 2)));
							Main.sunModY = (short)(Main.mouseState.Y - num10);
							if (Main.time > 53990.0)
							{
								Main.time = 53990.0;
							}
						}
						else
						{
							Main.time = 32400.0 * (double)((float)(Main.mouseState.X + Main.moonTexture.Width) / ((float)Main.screenWidth + (float)(Main.moonTexture.Width * 2)));
							Main.moonModY = (short)(Main.mouseState.Y - num13);
							if (Main.time > 32390.0)
							{
								Main.time = 32390.0;
							}
						}
						if (Main.time < 10.0)
						{
							Main.time = 10.0;
						}
						if (Main.netMode != 0)
						{
							NetMessage.SendData(18, -1, -1, "", 0, 0f, 0f, 0f, 0);
						}
						Main.grabSky = true;
					}
				}
				else
				{
					Main.grabSky = false;
				}
			}
			if (Main.resetClouds)
			{
				Cloud.resetClouds();
				Main.resetClouds = false;
			}
			if (base.IsActive || Main.netMode != 0)
			{
				Main.windSpeedSpeed += (float)Main.rand.Next(-10, 11) * 0.0001f;
				if ((double)Main.windSpeedSpeed < -0.002)
				{
					Main.windSpeedSpeed = -0.002f;
				}
				if ((double)Main.windSpeedSpeed > 0.002)
				{
					Main.windSpeedSpeed = 0.002f;
				}
				Main.windSpeed += Main.windSpeedSpeed;
				if ((double)Main.windSpeed < -0.3)
				{
					Main.windSpeed = -0.3f;
				}
				if ((double)Main.windSpeed > 0.3)
				{
					Main.windSpeed = 0.3f;
				}
				Main.numClouds += Main.rand.Next(-1, 2);
				if (Main.numClouds < 0)
				{
					Main.numClouds = 0;
				}
				if (Main.numClouds > Main.cloudLimit)
				{
					Main.numClouds = Main.cloudLimit;
				}
			}
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int k = 0; k < 100; k++)
				{
					if (Main.cloud[k].active)
					{
						int num32 = (int)(40f * (2f - Main.cloud[k].scale));
						int num33 = 0;
						Color color2 = default(Color);
						num33 = (int)white.R;
						num33 -= num32;
						if (num33 <= 0)
						{
							num33 = 0;
						}
						color2.R = (byte)num33;
						num33 = (int)white.G;
						num33 -= num32;
						if (num33 <= 0)
						{
							num33 = 0;
						}
						color2.G = (byte)num33;
						num33 = (int)white.B;
						num33 -= num32;
						if (num33 <= 0)
						{
							num33 = 0;
						}
						color2.B = (byte)num33;
						color2.A = (byte)(255 - num32);
						float num34 = Main.cloud[k].position.Y * ((float)Main.screenHeight / 600f);
						this.spriteBatch.Draw(Main.cloudTexture[Main.cloud[k].type], new Vector2(Main.cloud[k].position.X + (float)Main.cloudTexture[Main.cloud[k].type].Width * 0.5f, num34 + (float)Main.cloudTexture[Main.cloud[k].type].Height * 0.5f + (float)num8), new Rectangle?(new Rectangle(0, 0, Main.cloudTexture[Main.cloud[k].type].Width, Main.cloudTexture[Main.cloud[k].type].Height)), color2, Main.cloud[k].rotation, new Vector2((float)Main.cloudTexture[Main.cloud[k].type].Width * 0.5f, (float)Main.cloudTexture[Main.cloud[k].type].Height * 0.5f), Main.cloud[k].scale, SpriteEffects.None, 0f);
					}
				}
			}
			if (Main.gameMenu || Main.netMode == 2)
			{
				this.DrawMenu();
				return;
			}
			int num35 = (int)(Main.screenPosition.X / 16f - 1f);
			int num36 = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			int num37 = (int)(Main.screenPosition.Y / 16f - 1f);
			int num38 = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			if (num35 < 0)
			{
				num35 = 0;
			}
			if (num36 > Main.maxTilesX)
			{
				num36 = Main.maxTilesX;
			}
			if (num37 < 0)
			{
				num37 = 0;
			}
			if (num38 > Main.maxTilesY)
			{
				num38 = Main.maxTilesY;
			}
			Lighting.LightTiles(num35, num36, num37, num38);
			Color arg_1D14_0 = Color.White;
			if (Main.ignoreErrors)
			{
				try
				{
					this.DrawWater(true);
					goto IL_1D2F;
				}
				catch
				{
					goto IL_1D2F;
				}
			}
			this.DrawWater(true);
			IL_1D2F:
			num3 = (double)Main.caveParrallax;
			num4 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[1]) - (double)(Main.backgroundWidth[1] / 2));
			num5 = Main.screenWidth / Main.backgroundWidth[1] + 2;
			num8 = (int)((float)((int)Main.worldSurface * 16 - Main.backgroundHeight[1]) - Main.screenPosition.Y + 16f);
			for (int l = 0; l < num5; l++)
			{
				for (int m = 0; m < 6; m++)
				{
					int num39 = (num4 + Main.backgroundWidth[1] * l + m * 16) / 16;
					int num40 = num8 / 16;
					Color color3 = Lighting.GetColor(num39 + (int)(Main.screenPosition.X / 16f), num40 + (int)(Main.screenPosition.Y / 16f));
					this.spriteBatch.Draw(Main.backgroundTexture[1], new Vector2((float)(num4 + Main.backgroundWidth[1] * l + 16 * m), (float)num8), new Rectangle?(new Rectangle(16 * m, 0, 16, 16)), color3);
				}
			}
			double num41 = (double)(Main.maxTilesY - 230);
			double num42 = (double)((int)((num41 - Main.worldSurface) / 6.0) * 6);
			num41 = Main.worldSurface + num42 - 5.0;
			bool flag2 = false;
			bool flag3 = false;
			num8 = (int)((float)((int)Main.worldSurface * 16) - Main.screenPosition.Y + 16f);
			if (Main.worldSurface * 16.0 <= (double)(Main.screenPosition.Y + (float)Main.screenHeight))
			{
				num3 = (double)Main.caveParrallax;
				num4 = (int)(-Math.IEEERemainder(100.0 + (double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[2]) - (double)(Main.backgroundWidth[2] / 2));
				num5 = Main.screenWidth / Main.backgroundWidth[2] + 2;
				if (Main.worldSurface * 16.0 < (double)(Main.screenPosition.Y - 16f))
				{
					num6 = (int)(Math.IEEERemainder((double)num8, (double)Main.backgroundHeight[2]) - (double)Main.backgroundHeight[2]);
					num7 = (Main.screenHeight - num6) / Main.backgroundHeight[2] + 1;
				}
				else
				{
					num6 = num8;
					num7 = (Main.screenHeight - num8) / Main.backgroundHeight[2] + 1;
				}
				if (Main.rockLayer * 16.0 < (double)(Main.screenPosition.Y + 600f))
				{
					num7 = (int)(Main.rockLayer * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)num6) / Main.backgroundHeight[2];
					flag3 = true;
				}
				for (int n = 0; n < num5; n++)
				{
					for (int num43 = 0; num43 < num7; num43++)
					{
						this.spriteBatch.Draw(Main.backgroundTexture[2], new Rectangle(num4 + Main.backgroundWidth[2] * n, num6 + Main.backgroundHeight[2] * num43, Main.backgroundWidth[2], Main.backgroundHeight[2]), Color.White);
					}
				}
				if (flag3)
				{
					num3 = (double)Main.caveParrallax;
					num4 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[1]) - (double)(Main.backgroundWidth[1] / 2));
					num5 = Main.screenWidth / Main.backgroundWidth[1] + 2;
					num8 = num6 + num7 * Main.backgroundHeight[2];
					for (int num44 = 0; num44 < num5; num44++)
					{
						for (int num45 = 0; num45 < 6; num45++)
						{
							int arg_20C6_0 = (num4 + Main.backgroundWidth[4] * num44 + num45 * 16) / 16;
							int arg_20CC_0 = num8 / 16;
							this.spriteBatch.Draw(Main.backgroundTexture[4], new Vector2((float)(num4 + Main.backgroundWidth[4] * num44 + 16 * num45), (float)num8), new Rectangle?(new Rectangle(16 * num45, 0, 16, 24)), Color.White);
						}
					}
				}
			}
			num8 = (int)((float)((int)Main.rockLayer * 16) - Main.screenPosition.Y + 16f + 600f);
			if (Main.rockLayer * 16.0 <= (double)(Main.screenPosition.Y + 600f))
			{
				num3 = (double)Main.caveParrallax;
				num4 = (int)(-Math.IEEERemainder(100.0 + (double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[3]) - (double)(Main.backgroundWidth[3] / 2));
				num5 = Main.screenWidth / Main.backgroundWidth[3] + 2;
				if (Main.rockLayer * 16.0 + (double)Main.screenHeight < (double)(Main.screenPosition.Y - 16f))
				{
					num6 = (int)(Math.IEEERemainder((double)num8, (double)Main.backgroundHeight[3]) - (double)Main.backgroundHeight[3]);
					num7 = (Main.screenHeight - num6) / Main.backgroundHeight[3] + 1;
				}
				else
				{
					num6 = num8;
					num7 = (Main.screenHeight - num8) / Main.backgroundHeight[3] + 1;
				}
				if (num41 * 16.0 < (double)(Main.screenPosition.Y + 600f))
				{
					num7 = (int)(num41 * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)num6) / Main.backgroundHeight[2];
					flag2 = true;
				}
				for (int num46 = 0; num46 < num5; num46++)
				{
					for (int num47 = 0; num47 < num7; num47++)
					{
						this.spriteBatch.Draw(Main.backgroundTexture[3], new Rectangle(num4 + Main.backgroundWidth[2] * num46, num6 + Main.backgroundHeight[2] * num47, Main.backgroundWidth[2], Main.backgroundHeight[2]), Color.White);
					}
				}
				if (flag2)
				{
					num3 = (double)Main.caveParrallax;
					num4 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[1]) - (double)(Main.backgroundWidth[1] / 2) - 4.0);
					num5 = Main.screenWidth / Main.backgroundWidth[1] + 2;
					num8 = num6 + num7 * Main.backgroundHeight[2];
					for (int num48 = 0; num48 < num5; num48++)
					{
						for (int num49 = 0; num49 < 6; num49++)
						{
							int num50 = (num4 + Main.backgroundWidth[1] * num48 + num49 * 16) / 16;
							int num51 = num8 / 16;
							Lighting.GetColor(num50 + (int)(Main.screenPosition.X / 16f), num51 + (int)(Main.screenPosition.Y / 16f));
							this.spriteBatch.Draw(Main.backgroundTexture[6], new Vector2((float)(num4 + Main.backgroundWidth[1] * num48 + 16 * num49), (float)num8), new Rectangle?(new Rectangle(16 * num49, Main.magmaBGFrame * 24, 16, 24)), Color.White);
						}
					}
				}
			}
			num8 = (int)((float)((int)num41 * 16) - Main.screenPosition.Y + 16f + 600f) + 8;
			if (num41 * 16.0 <= (double)(Main.screenPosition.Y + 600f))
			{
				num4 = (int)(-Math.IEEERemainder(100.0 + (double)Main.screenPosition.X * num3, (double)Main.backgroundWidth[3]) - (double)(Main.backgroundWidth[3] / 2));
				num5 = Main.screenWidth / Main.backgroundWidth[3] + 2;
				if (num41 * 16.0 + (double)Main.screenHeight < (double)(Main.screenPosition.Y - 16f))
				{
					num6 = (int)(Math.IEEERemainder((double)num8, (double)Main.backgroundHeight[3]) - (double)Main.backgroundHeight[3]);
					num7 = (Main.screenHeight - num6) / Main.backgroundHeight[3] + 1;
				}
				else
				{
					num6 = num8;
					num7 = (Main.screenHeight - num8) / Main.backgroundHeight[3] + 1;
				}
				for (int num52 = 0; num52 < num5; num52++)
				{
					for (int num53 = 0; num53 < num7; num53++)
					{
						SpriteBatch arg_258D_0 = this.spriteBatch;
						Texture2D arg_258D_1 = Main.backgroundTexture[5];
						Vector2 arg_258D_2 = new Vector2((float)(num4 + Main.backgroundWidth[2] * num52), (float)(num6 + Main.backgroundHeight[2] * num53));
						Rectangle? arg_258D_3 = new Rectangle?(new Rectangle(0, Main.backgroundHeight[2] * Main.magmaBGFrame, Main.backgroundWidth[2], Main.backgroundHeight[2]));
						Color arg_258D_4 = Color.White;
						float arg_258D_5 = 0f;
						Vector2 origin = default(Vector2);
						arg_258D_0.Draw(arg_258D_1, arg_258D_2, arg_258D_3, arg_258D_4, arg_258D_5, origin, 1f, SpriteEffects.None, 0f);
					}
				}
			}
			Main.magmaBGFrameCounter++;
			if (Main.magmaBGFrameCounter >= 8)
			{
				Main.magmaBGFrameCounter = 0;
				Main.magmaBGFrame++;
				if (Main.magmaBGFrame >= 3)
				{
					Main.magmaBGFrame = 0;
				}
			}
			try
			{
				for (int num54 = num37; num54 < num38 + 4; num54++)
				{
					for (int num55 = num35 - 2; num55 < num36 + 2; num55++)
					{
						if (Main.tile[num55, num54] == null)
						{
							Main.tile[num55, num54] = new Tile();
						}
						if (Lighting.Brightness(num55, num54) * 255f < (float)(Main.tileColor.R - 12) || (double)num54 > Main.worldSurface)
						{
							SpriteBatch arg_26D2_0 = this.spriteBatch;
							Texture2D arg_26D2_1 = Main.blackTileTexture;
							Vector2 arg_26D2_2 = new Vector2((float)(num55 * 16 - (int)Main.screenPosition.X), (float)(num54 * 16 - (int)Main.screenPosition.Y));
							Rectangle? arg_26D2_3 = new Rectangle?(new Rectangle((int)Main.tile[num55, num54].frameX, (int)Main.tile[num55, num54].frameY, 16, 16));
							Color arg_26D2_4 = Lighting.GetBlackness(num55, num54);
							float arg_26D2_5 = 0f;
							Vector2 origin = default(Vector2);
							arg_26D2_0.Draw(arg_26D2_1, arg_26D2_2, arg_26D2_3, arg_26D2_4, arg_26D2_5, origin, 1f, SpriteEffects.None, 0f);
						}
					}
				}
				for (int num56 = num37; num56 < num38 + 4; num56++)
				{
					for (int num57 = num35 - 2; num57 < num36 + 2; num57++)
					{
						if (Main.tile[num57, num56].wall > 0 && Lighting.Brightness(num57, num56) > 0f)
						{
							if (Main.tile[num57, num56].wallFrameY == 18 && Main.tile[num57, num56].wallFrameX >= 18)
							{
								byte arg_277A_0 = Main.tile[num57, num56].wallFrameY;
							}
							Rectangle value2 = new Rectangle((int)(Main.tile[num57, num56].wallFrameX * 2), (int)(Main.tile[num57, num56].wallFrameY * 2), 32, 32);
							SpriteBatch arg_2826_0 = this.spriteBatch;
							Texture2D arg_2826_1 = Main.wallTexture[(int)Main.tile[num57, num56].wall];
							Vector2 arg_2826_2 = new Vector2((float)(num57 * 16 - (int)Main.screenPosition.X - 8), (float)(num56 * 16 - (int)Main.screenPosition.Y - 8));
							Rectangle? arg_2826_3 = new Rectangle?(value2);
							Color arg_2826_4 = Lighting.GetColor(num57, num56);
							float arg_2826_5 = 0f;
							Vector2 origin = default(Vector2);
							arg_2826_0.Draw(arg_2826_1, arg_2826_2, arg_2826_3, arg_2826_4, arg_2826_5, origin, 1f, SpriteEffects.None, 0f);
						}
					}
				}
				if (Main.player[Main.myPlayer].detectCreature)
				{
					this.DrawTiles(false);
					this.DrawTiles(true);
					this.DrawGore();
					this.DrawNPCs(true);
					this.DrawNPCs(false);
				}
				else
				{
					this.DrawTiles(false);
					this.DrawNPCs(true);
					this.DrawTiles(true);
					this.DrawGore();
					this.DrawNPCs(false);
				}
			}
			catch
			{
			}
			for (int num58 = 0; num58 < 1000; num58++)
			{
				if (Main.projectile[num58].active && Main.projectile[num58].type > 0 && !Main.projectile[num58].hide)
				{
					this.DrawProj(num58);
				}
			}
			for (int num59 = 0; num59 < 255; num59++)
			{
				if (Main.player[num59].active)
				{
					if (Main.player[num59].ghost)
					{
						Vector2 position = Main.player[num59].position;
						Main.player[num59].position = Main.player[num59].shadowPos[0];
						Main.player[num59].shadow = 0.5f;
						this.DrawGhost(Main.player[num59]);
						Main.player[num59].position = Main.player[num59].shadowPos[1];
						Main.player[num59].shadow = 0.7f;
						this.DrawGhost(Main.player[num59]);
						Main.player[num59].position = Main.player[num59].shadowPos[2];
						Main.player[num59].shadow = 0.9f;
						this.DrawGhost(Main.player[num59]);
						Main.player[num59].position = position;
						Main.player[num59].shadow = 0f;
						this.DrawGhost(Main.player[num59]);
					}
					else
					{
						if ((Main.player[num59].head == 5 && Main.player[num59].body == 5 && Main.player[num59].legs == 5) || (Main.player[num59].head == 7 && Main.player[num59].body == 7 && Main.player[num59].legs == 7) || (Main.player[num59].head == 22 && Main.player[num59].body == 14 && Main.player[num59].legs == 14))
						{
							Vector2 position2 = Main.player[num59].position;
							Main.player[num59].position = Main.player[num59].shadowPos[0];
							Main.player[num59].shadow = 0.5f;
							this.DrawPlayer(Main.player[num59]);
							Main.player[num59].position = Main.player[num59].shadowPos[1];
							Main.player[num59].shadow = 0.7f;
							this.DrawPlayer(Main.player[num59]);
							Main.player[num59].position = Main.player[num59].shadowPos[2];
							Main.player[num59].shadow = 0.9f;
							this.DrawPlayer(Main.player[num59]);
							Main.player[num59].position = position2;
							Main.player[num59].shadow = 0f;
						}
						this.DrawPlayer(Main.player[num59]);
					}
				}
			}
			for (int num60 = 0; num60 < 200; num60++)
			{
				if (Main.item[num60].active && Main.item[num60].type > 0)
				{
					int arg_2C4A_0 = (int)((double)Main.item[num60].position.X + (double)Main.item[num60].width * 0.5) / 16;
					int arg_2C7B_0 = (int)((double)Main.item[num60].position.Y + (double)Main.item[num60].height * 0.5) / 16;
					Color color4 = Lighting.GetColor((int)((double)Main.item[num60].position.X + (double)Main.item[num60].width * 0.5) / 16, (int)((double)Main.item[num60].position.Y + (double)Main.item[num60].height * 0.5) / 16);
					Color color5;
					if (!Main.gamePaused && base.IsActive && ((Main.item[num60].type >= 71 && Main.item[num60].type <= 74) || Main.item[num60].type == 58 || Main.item[num60].type == 109) && color4.R > 60)
					{
						float num61 = (float)Main.rand.Next(500) - (Math.Abs(Main.item[num60].velocity.X) + Math.Abs(Main.item[num60].velocity.Y)) * 10f;
						if (num61 < (float)(color4.R / 50))
						{
							Vector2 arg_2DEB_0 = Main.item[num60].position;
							int arg_2DEB_1 = Main.item[num60].width;
							int arg_2DEB_2 = Main.item[num60].height;
							int arg_2DEB_3 = 43;
							float arg_2DEB_4 = 0f;
							float arg_2DEB_5 = 0f;
							int arg_2DEB_6 = 254;
							color5 = default(Color);
							int num62 = Dust.NewDust(arg_2DEB_0, arg_2DEB_1, arg_2DEB_2, arg_2DEB_3, arg_2DEB_4, arg_2DEB_5, arg_2DEB_6, color5, 0.5f);
							Dust expr_2DFA = Main.dust[num62];
							expr_2DFA.velocity *= 0f;
						}
					}
					SpriteBatch arg_2F26_0 = this.spriteBatch;
					Texture2D arg_2F26_1 = Main.itemTexture[Main.item[num60].type];
					Vector2 arg_2F26_2 = new Vector2(Main.item[num60].position.X - Main.screenPosition.X + (float)(Main.item[num60].width / 2) - (float)(Main.itemTexture[Main.item[num60].type].Width / 2), Main.item[num60].position.Y - Main.screenPosition.Y + (float)(Main.item[num60].height / 2) - (float)(Main.itemTexture[Main.item[num60].type].Height / 2));
					Rectangle? arg_2F26_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.item[num60].type].Width, Main.itemTexture[Main.item[num60].type].Height));
					Color arg_2F26_4 = Main.item[num60].GetAlpha(color4);
					float arg_2F26_5 = 0f;
					Vector2 origin = default(Vector2);
					arg_2F26_0.Draw(arg_2F26_1, arg_2F26_2, arg_2F26_3, arg_2F26_4, arg_2F26_5, origin, 1f, SpriteEffects.None, 0f);
					Color arg_2F42_0 = Main.item[num60].color;
					color5 = default(Color);
					if (arg_2F42_0 != color5)
					{
						SpriteBatch arg_3063_0 = this.spriteBatch;
						Texture2D arg_3063_1 = Main.itemTexture[Main.item[num60].type];
						Vector2 arg_3063_2 = new Vector2(Main.item[num60].position.X - Main.screenPosition.X + (float)(Main.item[num60].width / 2) - (float)(Main.itemTexture[Main.item[num60].type].Width / 2), Main.item[num60].position.Y - Main.screenPosition.Y + (float)(Main.item[num60].height / 2) - (float)(Main.itemTexture[Main.item[num60].type].Height / 2));
						Rectangle? arg_3063_3 = new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.item[num60].type].Width, Main.itemTexture[Main.item[num60].type].Height));
						Color arg_3063_4 = Main.item[num60].GetColor(color4);
						float arg_3063_5 = 0f;
						origin = default(Vector2);
						arg_3063_0.Draw(arg_3063_1, arg_3063_2, arg_3063_3, arg_3063_4, arg_3063_5, origin, 1f, SpriteEffects.None, 0f);
					}
				}
			}
			Rectangle value3 = new Rectangle((int)Main.screenPosition.X - 50, (int)Main.screenPosition.Y - 50, Main.screenWidth + 100, Main.screenHeight + 100);
			for (int num63 = 0; num63 < Main.numDust; num63++)
			{
				if (Main.dust[num63].active)
				{
					if (new Rectangle((int)Main.dust[num63].position.X, (int)Main.dust[num63].position.Y, 4, 4).Intersects(value3))
					{
						Color color6 = Lighting.GetColor((int)((double)Main.dust[num63].position.X + 4.0) / 16, (int)((double)Main.dust[num63].position.Y + 4.0) / 16);
						if (Main.dust[num63].type == 6 || Main.dust[num63].type == 15 || Main.dust[num63].noLight)
						{
							color6 = Color.White;
						}
						color6 = Main.dust[num63].GetAlpha(color6);
						this.spriteBatch.Draw(Main.dustTexture, Main.dust[num63].position - Main.screenPosition, new Rectangle?(Main.dust[num63].frame), color6, Main.dust[num63].rotation, new Vector2(4f, 4f), Main.dust[num63].scale, SpriteEffects.None, 0f);
						Color arg_3216_0 = Main.dust[num63].color;
						Color color5 = default(Color);
						if (arg_3216_0 != color5)
						{
							this.spriteBatch.Draw(Main.dustTexture, Main.dust[num63].position - Main.screenPosition, new Rectangle?(Main.dust[num63].frame), Main.dust[num63].GetColor(color6), Main.dust[num63].rotation, new Vector2(4f, 4f), Main.dust[num63].scale, SpriteEffects.None, 0f);
						}
						if (color6 == Color.Black)
						{
							Main.dust[num63].active = false;
						}
					}
					else
					{
						Main.dust[num63].active = false;
					}
				}
			}
			if (Main.ignoreErrors)
			{
				try
				{
					this.DrawWater(false);
					goto IL_32EC;
				}
				catch
				{
					goto IL_32EC;
				}
				goto IL_32E5;
			}
			goto IL_32E5;
			IL_32EC:
			if (!Main.hideUI)
			{
				for (int num64 = 0; num64 < 255; num64++)
				{
					if (Main.player[num64].active && Main.player[num64].chatShowTime > 0 && num64 != Main.myPlayer && !Main.player[num64].dead)
					{
						Vector2 vector = Main.fontMouseText.MeasureString(Main.player[num64].chatText);
						Vector2 vector2;
						vector2.X = Main.player[num64].position.X + (float)(Main.player[num64].width / 2) - vector.X / 2f;
						vector2.Y = Main.player[num64].position.Y - vector.Y - 2f;
						for (int num65 = 0; num65 < 5; num65++)
						{
							int num66 = 0;
							int num67 = 0;
							Color black = Color.Black;
							if (num65 == 0)
							{
								num66 = -2;
							}
							if (num65 == 1)
							{
								num66 = 2;
							}
							if (num65 == 2)
							{
								num67 = -2;
							}
							if (num65 == 3)
							{
								num67 = 2;
							}
							if (num65 == 4)
							{
								black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
							}
							SpriteBatch arg_3474_0 = this.spriteBatch;
							SpriteFont arg_3474_1 = Main.fontMouseText;
							string arg_3474_2 = Main.player[num64].chatText;
							Vector2 arg_3474_3 = new Vector2(vector2.X + (float)num66 - Main.screenPosition.X, vector2.Y + (float)num67 - Main.screenPosition.Y);
							Color arg_3474_4 = black;
							float arg_3474_5 = 0f;
							Vector2 origin = default(Vector2);
							arg_3474_0.DrawString(arg_3474_1, arg_3474_2, arg_3474_3, arg_3474_4, arg_3474_5, origin, 1f, SpriteEffects.None, 0f);
						}
					}
				}
				for (int num68 = 0; num68 < 100; num68++)
				{
					if (Main.combatText[num68].active)
					{
						int num69 = 0;
						if (Main.combatText[num68].crit)
						{
							num69 = 1;
						}
						Vector2 vector3 = Main.fontCombatText[num69].MeasureString(Main.combatText[num68].text);
						Vector2 origin2 = new Vector2(vector3.X * 0.5f, vector3.Y * 0.5f);
						float arg_3512_0 = Main.combatText[num68].scale;
						float num70 = (float)Main.combatText[num68].color.R;
						float num71 = (float)Main.combatText[num68].color.G;
						float num72 = (float)Main.combatText[num68].color.B;
						float num73 = (float)Main.combatText[num68].color.A;
						num70 *= Main.combatText[num68].scale * Main.combatText[num68].alpha * 0.3f;
						num72 *= Main.combatText[num68].scale * Main.combatText[num68].alpha * 0.3f;
						num71 *= Main.combatText[num68].scale * Main.combatText[num68].alpha * 0.3f;
						num73 *= Main.combatText[num68].scale * Main.combatText[num68].alpha;
						Color color7 = new Color((int)num70, (int)num71, (int)num72, (int)num73);
						for (int num74 = 0; num74 < 5; num74++)
						{
							int num75 = 0;
							int num76 = 0;
							if (num74 == 0)
							{
								num75--;
							}
							else
							{
								if (num74 == 1)
								{
									num75++;
								}
								else
								{
									if (num74 == 2)
									{
										num76--;
									}
									else
									{
										if (num74 == 3)
										{
											num76++;
										}
										else
										{
											num70 = (float)Main.combatText[num68].color.R * Main.combatText[num68].scale * Main.combatText[num68].alpha;
											num72 = (float)Main.combatText[num68].color.B * Main.combatText[num68].scale * Main.combatText[num68].alpha;
											num71 = (float)Main.combatText[num68].color.G * Main.combatText[num68].scale * Main.combatText[num68].alpha;
											num73 = (float)Main.combatText[num68].color.A * Main.combatText[num68].scale * Main.combatText[num68].alpha;
											color7 = new Color((int)num70, (int)num71, (int)num72, (int)num73);
										}
									}
								}
							}
							this.spriteBatch.DrawString(Main.fontCombatText[num69], Main.combatText[num68].text, new Vector2(Main.combatText[num68].position.X - Main.screenPosition.X + (float)num75 + origin2.X, Main.combatText[num68].position.Y - Main.screenPosition.Y + (float)num76 + origin2.Y), color7, Main.combatText[num68].rotation, origin2, Main.combatText[num68].scale, SpriteEffects.None, 0f);
						}
					}
				}
				for (int num77 = 0; num77 < 100; num77++)
				{
					if (Main.itemText[num77].active)
					{
						string text = Main.itemText[num77].name;
						if (Main.itemText[num77].stack > 1)
						{
							text = string.Concat(new object[]
							{
								text, 
								" (", 
								Main.itemText[num77].stack, 
								")"
							});
						}
						Vector2 vector4 = Main.fontMouseText.MeasureString(text);
						Vector2 origin3 = new Vector2(vector4.X * 0.5f, vector4.Y * 0.5f);
						float arg_389C_0 = Main.itemText[num77].scale;
						float num78 = (float)Main.itemText[num77].color.R;
						float num79 = (float)Main.itemText[num77].color.G;
						float num80 = (float)Main.itemText[num77].color.B;
						float num81 = (float)Main.itemText[num77].color.A;
						num78 *= Main.itemText[num77].scale * Main.itemText[num77].alpha * 0.3f;
						num80 *= Main.itemText[num77].scale * Main.itemText[num77].alpha * 0.3f;
						num79 *= Main.itemText[num77].scale * Main.itemText[num77].alpha * 0.3f;
						num81 *= Main.itemText[num77].scale * Main.itemText[num77].alpha;
						Color color8 = new Color((int)num78, (int)num79, (int)num80, (int)num81);
						for (int num82 = 0; num82 < 5; num82++)
						{
							int num83 = 0;
							int num84 = 0;
							if (num82 == 0)
							{
								num83 -= 2;
							}
							else
							{
								if (num82 == 1)
								{
									num83 += 2;
								}
								else
								{
									if (num82 == 2)
									{
										num84 -= 2;
									}
									else
									{
										if (num82 == 3)
										{
											num84 += 2;
										}
										else
										{
											num78 = (float)Main.itemText[num77].color.R * Main.itemText[num77].scale * Main.itemText[num77].alpha;
											num80 = (float)Main.itemText[num77].color.B * Main.itemText[num77].scale * Main.itemText[num77].alpha;
											num79 = (float)Main.itemText[num77].color.G * Main.itemText[num77].scale * Main.itemText[num77].alpha;
											num81 = (float)Main.itemText[num77].color.A * Main.itemText[num77].scale * Main.itemText[num77].alpha;
											color8 = new Color((int)num78, (int)num79, (int)num80, (int)num81);
										}
									}
								}
							}
							if (num82 < 4)
							{
								num81 = (float)Main.itemText[num77].color.A * Main.itemText[num77].scale * Main.itemText[num77].alpha;
								color8 = new Color(0, 0, 0, (int)num81);
							}
							this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(Main.itemText[num77].position.X - Main.screenPosition.X + (float)num83 + origin3.X, Main.itemText[num77].position.Y - Main.screenPosition.Y + (float)num84 + origin3.Y), color8, Main.itemText[num77].rotation, origin3, Main.itemText[num77].scale, SpriteEffects.None, 0f);
						}
					}
				}
				if (Main.netMode == 1 && Netplay.clientSock.statusText != "" && Netplay.clientSock.statusText != null)
				{
					string text2 = string.Concat(new object[]
					{
						Netplay.clientSock.statusText, 
						": ", 
						(int)((float)Netplay.clientSock.statusCount / (float)Netplay.clientSock.statusMax * 100f), 
						"%"
					});
					SpriteBatch arg_3CA6_0 = this.spriteBatch;
					SpriteFont arg_3CA6_1 = Main.fontMouseText;
					string arg_3CA6_2 = text2;
					Vector2 arg_3CA6_3 = new Vector2(628f - Main.fontMouseText.MeasureString(text2).X * 0.5f + (float)(Main.screenWidth - 800), 84f);
					Color arg_3CA6_4 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
					float arg_3CA6_5 = 0f;
					Vector2 origin = default(Vector2);
					arg_3CA6_0.DrawString(arg_3CA6_1, arg_3CA6_2, arg_3CA6_3, arg_3CA6_4, arg_3CA6_5, origin, 1f, SpriteEffects.None, 0f);
				}
				this.DrawFPS();
				this.DrawInterface();
			}
			this.spriteBatch.End();
			if (Main.mouseState.LeftButton == ButtonState.Pressed)
			{
				Main.mouseLeftRelease = false;
				goto IL_3CDD;
			}
			Main.mouseLeftRelease = true;
			goto IL_3CDD;
			IL_32E5:
			this.DrawWater(false);
			goto IL_32EC;
			IL_3CDD:
			if (Main.mouseState.RightButton == ButtonState.Pressed)
			{
				Main.mouseRightRelease = false;
			}
			else
			{
				Main.mouseRightRelease = true;
			}
			if (Main.mouseState.RightButton != ButtonState.Pressed)
			{
				Main.stackSplit = 0;
			}
			if (Main.stackSplit > 0)
			{
				Main.stackSplit--;
			}
		}
		private static void UpdateInvasion()
		{
			if (Main.invasionType > 0)
			{
				if (Main.invasionSize <= 0)
				{
					Main.InvasionWarning();
					Main.invasionType = 0;
					Main.invasionDelay = 7;
				}
				if (Main.invasionX == (double)Main.spawnTileX)
				{
					return;
				}
				float num = 0.2f;
				if (Main.invasionX > (double)Main.spawnTileX)
				{
					Main.invasionX -= (double)num;
					if (Main.invasionX <= (double)Main.spawnTileX)
					{
						Main.invasionX = (double)Main.spawnTileX;
						Main.InvasionWarning();
					}
					else
					{
						Main.invasionWarn--;
					}
				}
				else
				{
					if (Main.invasionX < (double)Main.spawnTileX)
					{
						Main.invasionX += (double)num;
						if (Main.invasionX >= (double)Main.spawnTileX)
						{
							Main.invasionX = (double)Main.spawnTileX;
							Main.InvasionWarning();
						}
						else
						{
							Main.invasionWarn--;
						}
					}
				}
				if (Main.invasionWarn <= 0)
				{
					Main.invasionWarn = 3600;
					Main.InvasionWarning();
				}
			}
		}
		private static void InvasionWarning()
		{
			if (Main.invasionType == 0)
			{
				return;
			}
			string text = "";
			if (Main.invasionSize <= 0)
			{
				text = "The goblin army has been defeated!";
			}
			else
			{
				if (Main.invasionX < (double)Main.spawnTileX)
				{
					text = "A goblin army is approaching from the west!";
				}
				else
				{
					if (Main.invasionX > (double)Main.spawnTileX)
					{
						text = "A goblin army is approaching from the east!";
					}
					else
					{
						text = "The goblin army has arrived!";
					}
				}
			}
			if (Main.netMode == 0)
			{
				Main.NewText(text, 175, 75, 255);
				return;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, text, 255, 175f, 75f, 255f, 0);
			}
		}
		public static void StartInvasion()
		{
			if (Main.invasionType == 0 && Main.invasionDelay == 0)
			{
				int num = 0;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].statLifeMax >= 200)
					{
						num++;
					}
				}
				if (num > 0)
				{
					Main.invasionType = 1;
					Main.invasionSize = 100 + 50 * num;
					Main.invasionWarn = 0;
					if (Main.rand.Next(2) == 0)
					{
						Main.invasionX = 0.0;
						return;
					}
					Main.invasionX = (double)Main.maxTilesX;
				}
			}
		}
		private static void UpdateClient()
		{
			if (Main.myPlayer == 255)
			{
				Netplay.disconnect = true;
			}
			Main.netPlayCounter++;
			if (Main.netPlayCounter > 3600)
			{
				Main.netPlayCounter = 0;
			}
			if (Math.IEEERemainder((double)Main.netPlayCounter, 300.0) == 0.0)
			{
				NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
			}
			if (Math.IEEERemainder((double)Main.netPlayCounter, 600.0) == 0.0)
			{
				NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
			}
			if (Netplay.clientSock.active)
			{
				Netplay.clientSock.timeOut++;
				if (!Main.stopTimeOuts && Netplay.clientSock.timeOut > 60 * Main.timeOut)
				{
					Main.statusText = "Connection timed out";
					Netplay.disconnect = true;
				}
			}
			for (int i = 0; i < 200; i++)
			{
				if (Main.item[i].active && Main.item[i].owner == Main.myPlayer)
				{
					Main.item[i].FindOwner(i);
				}
			}
		}
		private static void UpdateServer()
		{
			Main.netPlayCounter++;
			if (Main.netPlayCounter > 3600)
			{
				NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
				NetMessage.syncPlayers();
				Main.netPlayCounter = 0;
			}
			for (int i = 0; i < Main.maxNetPlayers; i++)
			{
				if (Main.player[i].active && Netplay.serverSock[i].active)
				{
					Netplay.serverSock[i].SpamUpdate();
				}
			}
			Math.IEEERemainder((double)Main.netPlayCounter, 60.0);
			if (Math.IEEERemainder((double)Main.netPlayCounter, 360.0) == 0.0)
			{
				bool flag = true;
				int num = Main.lastItemUpdate;
				int num2 = 0;
				while (flag)
				{
					num++;
					if (num >= 200)
					{
						num = 0;
					}
					num2++;
					if (!Main.item[num].active || Main.item[num].owner == 255)
					{
						NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
					}
					if (num2 >= Main.maxItemUpdates || num == Main.lastItemUpdate)
					{
						flag = false;
					}
				}
				Main.lastItemUpdate = num;
			}
			for (int j = 0; j < 200; j++)
			{
				if (Main.item[j].active && (Main.item[j].owner == 255 || !Main.player[Main.item[j].owner].active))
				{
					Main.item[j].FindOwner(j);
				}
			}
			for (int k = 0; k < 255; k++)
			{
				if (Netplay.serverSock[k].active)
				{
					Netplay.serverSock[k].timeOut++;
					if (!Main.stopTimeOuts && Netplay.serverSock[k].timeOut > 60 * Main.timeOut)
					{
						Netplay.serverSock[k].kill = true;
					}
				}
				if (Main.player[k].active)
				{
					int sectionX = Netplay.GetSectionX((int)(Main.player[k].position.X / 16f));
					int sectionY = Netplay.GetSectionY((int)(Main.player[k].position.Y / 16f));
					int num3 = 0;
					for (int l = sectionX - 1; l < sectionX + 2; l++)
					{
						for (int m = sectionY - 1; m < sectionY + 2; m++)
						{
							if (l >= 0 && l < Main.maxSectionsX && m >= 0 && m < Main.maxSectionsY && !Netplay.serverSock[k].tileSection[l, m])
							{
								num3++;
							}
						}
					}
					if (num3 > 0)
					{
						int num4 = num3 * 150;
						NetMessage.SendData(9, k, -1, "Receiving tile data", num4, 0f, 0f, 0f, 0);
						Netplay.serverSock[k].statusText2 = "is receiving tile data";
						Netplay.serverSock[k].statusMax += num4;
						for (int n = sectionX - 1; n < sectionX + 2; n++)
						{
							for (int num5 = sectionY - 1; num5 < sectionY + 2; num5++)
							{
								if (n >= 0 && n < Main.maxSectionsX && num5 >= 0 && num5 < Main.maxSectionsY && !Netplay.serverSock[k].tileSection[n, num5])
								{
									NetMessage.SendSection(k, n, num5);
									NetMessage.SendData(11, k, -1, "", n, (float)num5, (float)n, (float)num5, 0);
								}
							}
						}
					}
				}
			}
		}
		public static void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			for (int i = Main.numChatLines - 1; i > 0; i--)
			{
				Main.chatLine[i].text = Main.chatLine[i - 1].text;
				Main.chatLine[i].showTime = Main.chatLine[i - 1].showTime;
				Main.chatLine[i].color = Main.chatLine[i - 1].color;
			}
			if (R == 0 && G == 0 && B == 0)
			{
				Main.chatLine[0].color = Color.White;
			}
			else
			{
				Main.chatLine[0].color = new Color((int)R, (int)G, (int)B);
			}
			Main.chatLine[0].text = newText;
			Main.chatLine[0].showTime = Main.chatLength;
			Main.PlaySound(12, -1, -1, 1);
		}
		private static void UpdateTime()
		{
			Main.time += 1.0;
			if (!Main.dayTime)
			{
				if (WorldGen.spawnEye && Main.netMode != 1 && Main.time > 4860.0)
				{
					for (int i = 0; i < 255; i++)
					{
						if (Main.player[i].active && !Main.player[i].dead && (double)Main.player[i].position.Y < Main.worldSurface * 16.0)
						{
							NPC.SpawnOnPlayer(i, 4);
							WorldGen.spawnEye = false;
							break;
						}
					}
				}
				if (Main.time > 32400.0)
				{
					if (Main.invasionDelay > 0)
					{
						Main.invasionDelay--;
					}
					WorldGen.spawnNPC = 0;
					Main.checkForSpawns = 0;
					Main.time = 0.0;
					Main.bloodMoon = false;
					Main.dayTime = true;
					Main.moonPhase++;
					if (Main.moonPhase >= 8)
					{
						Main.moonPhase = 0;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
						WorldGen.saveAndPlay();
					}
					if (Main.netMode != 1 && WorldGen.shadowOrbSmashed && Main.rand.Next(15) == 0)
					{
						Main.StartInvasion();
					}
				}
				if (Main.time > 16200.0 && WorldGen.spawnMeteor)
				{
					WorldGen.spawnMeteor = false;
					WorldGen.dropMeteor();
					return;
				}
			}
			else
			{
				Main.bloodMoon = false;
				if (Main.time > 54000.0)
				{
					WorldGen.spawnNPC = 0;
					Main.checkForSpawns = 0;
					if (Main.rand.Next(50) == 0 && Main.netMode != 1 && WorldGen.shadowOrbSmashed)
					{
						WorldGen.spawnMeteor = true;
					}
					if (!NPC.downedBoss1 && Main.netMode != 1)
					{
						bool flag = false;
						for (int j = 0; j < 255; j++)
						{
							if (Main.player[j].active && Main.player[j].statLifeMax >= 200 && Main.player[j].statDefense > 10)
							{
								flag = true;
								break;
							}
						}
						if (flag && Main.rand.Next(3) == 0)
						{
							int num = 0;
							for (int k = 0; k < 1000; k++)
							{
								if (Main.npc[k].active && Main.npc[k].townNPC)
								{
									num++;
								}
							}
							if (num >= 4)
							{
								WorldGen.spawnEye = true;
								if (Main.netMode == 0)
								{
									Main.NewText("You feel an evil presence watching you...", 50, 255, 130);
								}
								else
								{
									if (Main.netMode == 2)
									{
										NetMessage.SendData(25, -1, -1, "You feel an evil presence watching you...", 255, 50f, 255f, 130f, 0);
									}
								}
							}
						}
					}
					if (!WorldGen.spawnEye && Main.moonPhase != 4 && Main.rand.Next(7) == 0 && Main.netMode != 1)
					{
						for (int l = 0; l < 255; l++)
						{
							if (Main.player[l].active && Main.player[l].statLifeMax > 120)
							{
								Main.bloodMoon = true;
								break;
							}
						}
						if (Main.bloodMoon)
						{
							if (Main.netMode == 0)
							{
								Main.NewText("The Blood Moon is rising...", 50, 255, 130);
							}
							else
							{
								if (Main.netMode == 2)
								{
									NetMessage.SendData(25, -1, -1, "The Blood Moon is rising...", 255, 50f, 255f, 130f, 0);
								}
							}
						}
					}
					Main.time = 0.0;
					Main.dayTime = false;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
					}
				}
				if (Main.netMode != 1)
				{
					Main.checkForSpawns++;
					if (Main.checkForSpawns >= 7200)
					{
						int num2 = 0;
						for (int m = 0; m < 255; m++)
						{
							if (Main.player[m].active)
							{
								num2++;
							}
						}
						Main.checkForSpawns = 0;
						WorldGen.spawnNPC = 0;
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						int num7 = 0;
						int num8 = 0;
						int num9 = 0;
						int num10 = 0;
						int num11 = 0;
						for (int n = 0; n < 1000; n++)
						{
							if (Main.npc[n].active && Main.npc[n].townNPC)
							{
								if (Main.npc[n].type != 37 && !Main.npc[n].homeless)
								{
									WorldGen.QuickFindHome(n);
								}
								if (Main.npc[n].type == 37)
								{
									num8++;
								}
								if (Main.npc[n].type == 17)
								{
									num3++;
								}
								if (Main.npc[n].type == 18)
								{
									num4++;
								}
								if (Main.npc[n].type == 19)
								{
									num6++;
								}
								if (Main.npc[n].type == 20)
								{
									num5++;
								}
								if (Main.npc[n].type == 22)
								{
									num7++;
								}
								if (Main.npc[n].type == 38)
								{
									num9++;
								}
								if (Main.npc[n].type == 54)
								{
									num10++;
								}
								num11++;
							}
						}
						if (WorldGen.spawnNPC == 0)
						{
							int num12 = 0;
							bool flag2 = false;
							int num13 = 0;
							bool flag3 = false;
							bool flag4 = false;
							for (int num14 = 0; num14 < 255; num14++)
							{
								if (Main.player[num14].active)
								{
									for (int num15 = 0; num15 < 44; num15++)
									{
										if (Main.player[num14].inventory[num15] != null & Main.player[num14].inventory[num15].stack > 0)
										{
											if (Main.player[num14].inventory[num15].type == 71)
											{
												num12 += Main.player[num14].inventory[num15].stack;
											}
											if (Main.player[num14].inventory[num15].type == 72)
											{
												num12 += Main.player[num14].inventory[num15].stack * 100;
											}
											if (Main.player[num14].inventory[num15].type == 73)
											{
												num12 += Main.player[num14].inventory[num15].stack * 10000;
											}
											if (Main.player[num14].inventory[num15].type == 74)
											{
												num12 += Main.player[num14].inventory[num15].stack * 1000000;
											}
											if (Main.player[num14].inventory[num15].ammo == 14 || Main.player[num14].inventory[num15].useAmmo == 14)
											{
												flag3 = true;
											}
											if (Main.player[num14].inventory[num15].type == 166 || Main.player[num14].inventory[num15].type == 167 || Main.player[num14].inventory[num15].type == 168 || Main.player[num14].inventory[num15].type == 235)
											{
												flag4 = true;
											}
										}
									}
									int num16 = Main.player[num14].statLifeMax / 20;
									if (num16 > 5)
									{
										flag2 = true;
									}
									num13 += num16;
								}
							}
							if (!NPC.downedBoss3 && num8 == 0)
							{
								int num17 = NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37, 0);
								Main.npc[num17].homeless = false;
								Main.npc[num17].homeTileX = Main.dungeonX;
								Main.npc[num17].homeTileY = Main.dungeonY;
							}
							if (WorldGen.spawnNPC == 0 && num7 < 1)
							{
								WorldGen.spawnNPC = 22;
							}
							if (WorldGen.spawnNPC == 0 && (double)num12 > 5000.0 && num3 < 1)
							{
								WorldGen.spawnNPC = 17;
							}
							if (WorldGen.spawnNPC == 0 && flag2 && num4 < 1)
							{
								WorldGen.spawnNPC = 18;
							}
							if (WorldGen.spawnNPC == 0 && flag3 && num6 < 1)
							{
								WorldGen.spawnNPC = 19;
							}
							if (WorldGen.spawnNPC == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3) && num5 < 1)
							{
								WorldGen.spawnNPC = 20;
							}
							if (WorldGen.spawnNPC == 0 && flag4 && num3 > 0 && num9 < 1)
							{
								WorldGen.spawnNPC = 38;
							}
							if (WorldGen.spawnNPC == 0 && NPC.downedBoss3 && num10 < 1)
							{
								WorldGen.spawnNPC = 54;
							}
							if (WorldGen.spawnNPC == 0 && num12 > 100000 && num3 < 2 && num2 > 2)
							{
								WorldGen.spawnNPC = 17;
							}
							if (WorldGen.spawnNPC == 0 && num13 >= 20 && num4 < 2 && num2 > 2)
							{
								WorldGen.spawnNPC = 18;
							}
							if (WorldGen.spawnNPC == 0 && num12 > 5000000 && num3 < 3 && num2 > 4)
							{
								WorldGen.spawnNPC = 17;
							}
						}
					}
				}
			}
		}
		public static int DamageVar(float dmg)
		{
			float num = dmg * (1f + (float)Main.rand.Next(-15, 16) * 0.01f);
			return (int)Math.Round((double)num);
		}
		public static double CalculateDamage(int Damage, int Defense)
		{
			double num = (double)Damage - (double)Defense * 0.5;
			if (num < 1.0)
			{
				num = 1.0;
			}
			return num;
		}
		public static void PlaySound(int type, int x = -1, int y = -1, int Style = 1)
		{
			int num = Style;
			try
			{
				if (!Main.dedServ)
				{
					if (Main.soundVolume != 0f)
					{
						bool flag = false;
						float num2 = 1f;
						float num3 = 0f;
						if (x == -1 || y == -1)
						{
							flag = true;
						}
						else
						{
							if (WorldGen.gen)
							{
								return;
							}
							if (Main.netMode == 2)
							{
								return;
							}
							Rectangle value = new Rectangle((int)(Main.screenPosition.X - (float)(Main.screenWidth * 2)), (int)(Main.screenPosition.Y - (float)(Main.screenHeight * 2)), Main.screenWidth * 5, Main.screenHeight * 5);
							Rectangle rectangle = new Rectangle(x, y, 1, 1);
							Vector2 vector = new Vector2(Main.screenPosition.X + (float)Main.screenWidth * 0.5f, Main.screenPosition.Y + (float)Main.screenHeight * 0.5f);
							if (rectangle.Intersects(value))
							{
								flag = true;
							}
							if (flag)
							{
								num3 = ((float)x - vector.X) / ((float)Main.screenWidth * 0.5f);
								float num4 = Math.Abs((float)x - vector.X);
								float num5 = Math.Abs((float)y - vector.Y);
								float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
								num2 = 1f - num6 / ((float)Main.screenWidth * 1.5f);
							}
						}
						if (num3 < -1f)
						{
							num3 = -1f;
						}
						if (num3 > 1f)
						{
							num3 = 1f;
						}
						if (num2 > 1f)
						{
							num2 = 1f;
						}
						if (num2 > 0f)
						{
							if (flag)
							{
								num2 *= Main.soundVolume;
								if (type == 0)
								{
									int num7 = Main.rand.Next(3);
									Main.soundInstanceDig[num7].Stop();
									Main.soundInstanceDig[num7] = Main.soundDig[num7].CreateInstance();
									Main.soundInstanceDig[num7].Volume = num2;
									Main.soundInstanceDig[num7].Pan = num3;
									Main.soundInstanceDig[num7].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
									Main.soundInstanceDig[num7].Play();
								}
								else
								{
									if (type == 1)
									{
										int num8 = Main.rand.Next(3);
										Main.soundInstancePlayerHit[num8].Stop();
										Main.soundInstancePlayerHit[num8] = Main.soundPlayerHit[num8].CreateInstance();
										Main.soundInstancePlayerHit[num8].Volume = num2;
										Main.soundInstancePlayerHit[num8].Pan = num3;
										Main.soundInstancePlayerHit[num8].Play();
									}
									else
									{
										if (type == 2)
										{
											if (num == 1)
											{
												int num9 = Main.rand.Next(3);
												if (num9 == 1)
												{
													num = 18;
												}
												if (num9 == 2)
												{
													num = 19;
												}
											}
											if (num != 9 && num != 10)
											{
												Main.soundInstanceItem[num].Stop();
											}
											Main.soundInstanceItem[num] = Main.soundItem[num].CreateInstance();
											Main.soundInstanceItem[num].Volume = num2;
											Main.soundInstanceItem[num].Pan = num3;
											Main.soundInstanceItem[num].Pitch = (float)Main.rand.Next(-6, 7) * 0.01f;
											Main.soundInstanceItem[num].Play();
										}
										else
										{
											if (type == 3)
											{
												Main.soundInstanceNPCHit[num].Stop();
												Main.soundInstanceNPCHit[num] = Main.soundNPCHit[num].CreateInstance();
												Main.soundInstanceNPCHit[num].Volume = num2;
												Main.soundInstanceNPCHit[num].Pan = num3;
												Main.soundInstanceNPCHit[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
												Main.soundInstanceNPCHit[num].Play();
											}
											else
											{
												if (type == 4)
												{
													Main.soundInstanceNPCKilled[num] = Main.soundNPCKilled[num].CreateInstance();
													Main.soundInstanceNPCKilled[num].Volume = num2;
													Main.soundInstanceNPCKilled[num].Pan = num3;
													Main.soundInstanceNPCKilled[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
													Main.soundInstanceNPCKilled[num].Play();
												}
												else
												{
													if (type == 5)
													{
														Main.soundInstancePlayerKilled.Stop();
														Main.soundInstancePlayerKilled = Main.soundPlayerKilled.CreateInstance();
														Main.soundInstancePlayerKilled.Volume = num2;
														Main.soundInstancePlayerKilled.Pan = num3;
														Main.soundInstancePlayerKilled.Play();
													}
													else
													{
														if (type == 6)
														{
															Main.soundInstanceGrass.Stop();
															Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
															Main.soundInstanceGrass.Volume = num2;
															Main.soundInstanceGrass.Pan = num3;
															Main.soundInstanceGrass.Pitch = (float)Main.rand.Next(-30, 31) * 0.01f;
															Main.soundInstanceGrass.Play();
														}
														else
														{
															if (type == 7)
															{
																Main.soundInstanceGrab.Stop();
																Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
																Main.soundInstanceGrab.Volume = num2;
																Main.soundInstanceGrab.Pan = num3;
																Main.soundInstanceGrab.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																Main.soundInstanceGrab.Play();
															}
															else
															{
																if (type == 8)
																{
																	Main.soundInstanceDoorOpen.Stop();
																	Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
																	Main.soundInstanceDoorOpen.Volume = num2;
																	Main.soundInstanceDoorOpen.Pan = num3;
																	Main.soundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
																	Main.soundInstanceDoorOpen.Play();
																}
																else
																{
																	if (type == 9)
																	{
																		Main.soundInstanceDoorClosed.Stop();
																		Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
																		Main.soundInstanceDoorClosed.Volume = num2;
																		Main.soundInstanceDoorClosed.Pan = num3;
																		Main.soundInstanceDoorOpen.Pitch = (float)Main.rand.Next(-20, 21) * 0.01f;
																		Main.soundInstanceDoorClosed.Play();
																	}
																	else
																	{
																		if (type == 10)
																		{
																			Main.soundInstanceMenuOpen.Stop();
																			Main.soundInstanceMenuOpen = Main.soundMenuOpen.CreateInstance();
																			Main.soundInstanceMenuOpen.Volume = num2;
																			Main.soundInstanceMenuOpen.Pan = num3;
																			Main.soundInstanceMenuOpen.Play();
																		}
																		else
																		{
																			if (type == 11)
																			{
																				Main.soundInstanceMenuClose.Stop();
																				Main.soundInstanceMenuClose = Main.soundMenuClose.CreateInstance();
																				Main.soundInstanceMenuClose.Volume = num2;
																				Main.soundInstanceMenuClose.Pan = num3;
																				Main.soundInstanceMenuClose.Play();
																			}
																			else
																			{
																				if (type == 12)
																				{
																					Main.soundInstanceMenuTick.Stop();
																					Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
																					Main.soundInstanceMenuTick.Volume = num2;
																					Main.soundInstanceMenuTick.Pan = num3;
																					Main.soundInstanceMenuTick.Play();
																				}
																				else
																				{
																					if (type == 13)
																					{
																						Main.soundInstanceShatter.Stop();
																						Main.soundInstanceShatter = Main.soundShatter.CreateInstance();
																						Main.soundInstanceShatter.Volume = num2;
																						Main.soundInstanceShatter.Pan = num3;
																						Main.soundInstanceShatter.Play();
																					}
																					else
																					{
																						if (type == 14)
																						{
																							int num10 = Main.rand.Next(3);
																							Main.soundInstanceZombie[num10] = Main.soundZombie[num10].CreateInstance();
																							Main.soundInstanceZombie[num10].Volume = num2 * 0.4f;
																							Main.soundInstanceZombie[num10].Pan = num3;
																							Main.soundInstanceZombie[num10].Play();
																						}
																						else
																						{
																							if (type == 15)
																							{
																								if (Main.soundInstanceRoar[num].State == SoundState.Stopped)
																								{
																									Main.soundInstanceRoar[num] = Main.soundRoar[num].CreateInstance();
																									Main.soundInstanceRoar[num].Volume = num2;
																									Main.soundInstanceRoar[num].Pan = num3;
																									Main.soundInstanceRoar[num].Play();
																								}
																							}
																							else
																							{
																								if (type == 16)
																								{
																									Main.soundInstanceDoubleJump.Stop();
																									Main.soundInstanceDoubleJump = Main.soundDoubleJump.CreateInstance();
																									Main.soundInstanceDoubleJump.Volume = num2;
																									Main.soundInstanceDoubleJump.Pan = num3;
																									Main.soundInstanceDoubleJump.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																									Main.soundInstanceDoubleJump.Play();
																								}
																								else
																								{
																									if (type == 17)
																									{
																										Main.soundInstanceRun.Stop();
																										Main.soundInstanceRun = Main.soundRun.CreateInstance();
																										Main.soundInstanceRun.Volume = num2;
																										Main.soundInstanceRun.Pan = num3;
																										Main.soundInstanceRun.Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																										Main.soundInstanceRun.Play();
																									}
																									else
																									{
																										if (type == 18)
																										{
																											Main.soundInstanceCoins = Main.soundCoins.CreateInstance();
																											Main.soundInstanceCoins.Volume = num2;
																											Main.soundInstanceCoins.Pan = num3;
																											Main.soundInstanceCoins.Play();
																										}
																										else
																										{
																											if (type == 19)
																											{
																												if (Main.soundInstanceSplash[num].State == SoundState.Stopped)
																												{
																													Main.soundInstanceSplash[num] = Main.soundSplash[num].CreateInstance();
																													Main.soundInstanceSplash[num].Volume = num2;
																													Main.soundInstanceSplash[num].Pan = num3;
																													Main.soundInstanceSplash[num].Pitch = (float)Main.rand.Next(-10, 11) * 0.01f;
																													Main.soundInstanceSplash[num].Play();
																												}
																											}
																											else
																											{
																												if (type == 20)
																												{
																													int num11 = Main.rand.Next(3);
																													Main.soundInstanceFemaleHit[num11].Stop();
																													Main.soundInstanceFemaleHit[num11] = Main.soundFemaleHit[num11].CreateInstance();
																													Main.soundInstanceFemaleHit[num11].Volume = num2;
																													Main.soundInstanceFemaleHit[num11].Pan = num3;
																													Main.soundInstanceFemaleHit[num11].Play();
																												}
																												else
																												{
																													if (type == 21)
																													{
																														int num12 = Main.rand.Next(3);
																														Main.soundInstanceTink[num12].Stop();
																														Main.soundInstanceTink[num12] = Main.soundTink[num12].CreateInstance();
																														Main.soundInstanceTink[num12].Volume = num2;
																														Main.soundInstanceTink[num12].Pan = num3;
																														Main.soundInstanceTink[num12].Play();
																													}
																													else
																													{
																														if (type == 22)
																														{
																															Main.soundInstanceUnlock.Stop();
																															Main.soundInstanceUnlock = Main.soundUnlock.CreateInstance();
																															Main.soundInstanceUnlock.Volume = num2;
																															Main.soundInstanceUnlock.Pan = num3;
																															Main.soundInstanceUnlock.Play();
																														}
																														else
																														{
																															if (type == 23)
																															{
																																Main.soundInstanceDrown.Stop();
																																Main.soundInstanceDrown = Main.soundDrown.CreateInstance();
																																Main.soundInstanceDrown.Volume = num2;
																																Main.soundInstanceDrown.Pan = num3;
																																Main.soundInstanceDrown.Play();
																															}
																															else
																															{
																																if (type == 24)
																																{
																																	Main.soundInstanceChat = Main.soundChat.CreateInstance();
																																	Main.soundInstanceChat.Volume = num2;
																																	Main.soundInstanceChat.Pan = num3;
																																	Main.soundInstanceChat.Play();
																																}
																																else
																																{
																																	if (type == 25)
																																	{
																																		Main.soundInstanceMaxMana = Main.soundMaxMana.CreateInstance();
																																		Main.soundInstanceMaxMana.Volume = num2;
																																		Main.soundInstanceMaxMana.Pan = num3;
																																		Main.soundInstanceMaxMana.Play();
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
