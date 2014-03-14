using Microsoft.Xna.Framework;
using System;
namespace Freeria
{
	public class Cloud
	{
		public Vector2 position;
		public float scale;
		public float rotation;
		public float rSpeed;
		public float sSpeed;
		public bool active;
		public int type;
		public int width;
		public int height;
		private static Random rand = new Random();
		public static void resetClouds()
		{
			if (Main.cloudLimit < 10)
			{
				return;
			}
			Main.numClouds = Cloud.rand.Next(10, Main.cloudLimit);
			Main.windSpeed = 0f;
			while (Main.windSpeed == 0f)
			{
				Main.windSpeed = (float)Cloud.rand.Next(-100, 101) * 0.01f;
			}
			for (int i = 0; i < 100; i++)
			{
				Main.cloud[i].active = false;
			}
			for (int j = 0; j < Main.numClouds; j++)
			{
				Cloud.addCloud();
			}
			for (int k = 0; k < Main.numClouds; k++)
			{
				if (Main.windSpeed < 0f)
				{
					Cloud expr_9D_cp_0 = Main.cloud[k];
					expr_9D_cp_0.position.X = expr_9D_cp_0.position.X - (float)(Main.screenWidth * 2);
				}
				else
				{
					Cloud expr_BF_cp_0 = Main.cloud[k];
					expr_BF_cp_0.position.X = expr_BF_cp_0.position.X + (float)(Main.screenWidth * 2);
				}
			}
		}
		public static void addCloud()
		{
			int num = -1;
			for (int i = 0; i < 100; i++)
			{
				if (!Main.cloud[i].active)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				Main.cloud[num].rSpeed = 0f;
				Main.cloud[num].sSpeed = 0f;
				Main.cloud[num].type = Cloud.rand.Next(4);
				Main.cloud[num].scale = (float)Cloud.rand.Next(8, 13) * 0.1f;
				Main.cloud[num].rotation = (float)Cloud.rand.Next(-10, 11) * 0.01f;
				Main.cloud[num].width = (int)((float)Main.cloudTexture[Main.cloud[num].type].Width * Main.cloud[num].scale);
				Main.cloud[num].height = (int)((float)Main.cloudTexture[Main.cloud[num].type].Height * Main.cloud[num].scale);
				if (Main.windSpeed > 0f)
				{
					Main.cloud[num].position.X = (float)(-(float)Main.cloud[num].width - Main.cloudTexture[Main.cloud[num].type].Width - Cloud.rand.Next(Main.screenWidth * 2));
				}
				else
				{
					Main.cloud[num].position.X = (float)(Main.screenWidth + Main.cloudTexture[Main.cloud[num].type].Width + Cloud.rand.Next(Main.screenWidth * 2));
				}
				Main.cloud[num].position.Y = (float)Cloud.rand.Next((int)((float)(-(float)Main.screenHeight) * 0.25f), (int)((double)Main.screenHeight * 1.25));
				Cloud expr_1E5_cp_0 = Main.cloud[num];
				expr_1E5_cp_0.position.Y = expr_1E5_cp_0.position.Y - (float)Cloud.rand.Next((int)((float)Main.screenHeight * 0.25f));
				Cloud expr_215_cp_0 = Main.cloud[num];
				expr_215_cp_0.position.Y = expr_215_cp_0.position.Y - (float)Cloud.rand.Next((int)((float)Main.screenHeight * 0.25f));
				Main.cloud[num].scale *= 2.2f - (float)((double)(Main.cloud[num].position.Y + (float)Main.screenHeight * 0.25f) / ((double)Main.screenHeight * 1.5) + 0.699999988079071);
				if ((double)Main.cloud[num].scale > 1.4)
				{
					Main.cloud[num].scale = 1.4f;
				}
				if ((double)Main.cloud[num].scale < 0.6)
				{
					Main.cloud[num].scale = 0.6f;
				}
				Main.cloud[num].active = true;
				Rectangle rectangle = new Rectangle((int)Main.cloud[num].position.X, (int)Main.cloud[num].position.Y, Main.cloud[num].width, Main.cloud[num].height);
				for (int j = 0; j < 100; j++)
				{
					if (num != j && Main.cloud[j].active)
					{
						Rectangle value = new Rectangle((int)Main.cloud[j].position.X, (int)Main.cloud[j].position.Y, Main.cloud[j].width, Main.cloud[j].height);
						if (rectangle.Intersects(value))
						{
							Main.cloud[num].active = false;
						}
					}
				}
			}
		}
		public object Clone()
		{
			return base.MemberwiseClone();
		}
		public static void UpdateClouds()
		{
			int num = 0;
			for (int i = 0; i < 100; i++)
			{
				if (Main.cloud[i].active)
				{
					Main.cloud[i].Update();
					num++;
				}
			}
			for (int j = 0; j < 100; j++)
			{
				if (Main.cloud[j].active)
				{
					if (j > 1 && (!Main.cloud[j - 1].active || (double)Main.cloud[j - 1].scale > (double)Main.cloud[j].scale + 0.02))
					{
						Cloud cloud = (Cloud)Main.cloud[j - 1].Clone();
						Main.cloud[j - 1] = (Cloud)Main.cloud[j].Clone();
						Main.cloud[j] = cloud;
					}
					if (j < 99 && (!Main.cloud[j].active || (double)Main.cloud[j + 1].scale < (double)Main.cloud[j].scale - 0.02))
					{
						Cloud cloud2 = (Cloud)Main.cloud[j + 1].Clone();
						Main.cloud[j + 1] = (Cloud)Main.cloud[j].Clone();
						Main.cloud[j] = cloud2;
					}
				}
			}
			if (num < Main.numClouds)
			{
				Cloud.addCloud();
			}
		}
		public void Update()
		{
			if (Main.gameMenu)
			{
				this.position.X = this.position.X + Main.windSpeed * this.scale * 3f;
			}
			else
			{
				this.position.X = this.position.X + (Main.windSpeed - Main.player[Main.myPlayer].velocity.X * 0.1f) * this.scale;
			}
			if (Main.windSpeed > 0f)
			{
				if (this.position.X - (float)Main.cloudTexture[this.type].Width > (float)Main.screenWidth)
				{
					this.active = false;
				}
			}
			else
			{
				if (this.position.X + (float)this.width + (float)Main.cloudTexture[this.type].Width < 0f)
				{
					this.active = false;
				}
			}
			this.rSpeed += (float)Cloud.rand.Next(-10, 11) * 2E-05f;
			if ((double)this.rSpeed > 0.0007)
			{
				this.rSpeed = 0.0007f;
			}
			if ((double)this.rSpeed < -0.0007)
			{
				this.rSpeed = -0.0007f;
			}
			if ((double)this.rotation > 0.05)
			{
				this.rotation = 0.05f;
			}
			if ((double)this.rotation < -0.05)
			{
				this.rotation = -0.05f;
			}
			this.sSpeed += (float)Cloud.rand.Next(-10, 11) * 2E-05f;
			if ((double)this.sSpeed > 0.0007)
			{
				this.sSpeed = 0.0007f;
			}
			if ((double)this.sSpeed < -0.0007)
			{
				this.sSpeed = -0.0007f;
			}
			if ((double)this.scale > 1.4)
			{
				this.scale = 1.4f;
			}
			if ((double)this.scale < 0.6)
			{
				this.scale = 0.6f;
			}
			this.rotation += this.rSpeed;
			this.scale += this.sSpeed;
			this.width = (int)((float)Main.cloudTexture[this.type].Width * this.scale);
			this.height = (int)((float)Main.cloudTexture[this.type].Height * this.scale);
		}
	}
}
