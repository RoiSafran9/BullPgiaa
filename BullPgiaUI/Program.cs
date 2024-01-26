namespace BullPgiaUI
{
    using System.Windows.Forms;

    public class Program
    {
        public static void Main() 
        {
            Application.SetCompatibleTextRenderingDefault(false);

            bool m_StartNewGame = true;

            do
            {
                ChancesForm chancesForm = new ChancesForm();
                if (chancesForm.ShowDialog() == DialogResult.OK)
                {
                    GameBoardForm gameBoardForm = new GameBoardForm(chancesForm);
                    Application.Run(gameBoardForm);

                    m_StartNewGame = gameBoardForm.UserWantsNewGame;
                }
                else
                {
                    m_StartNewGame = false;
                }

            } while (m_StartNewGame); 
        }
    }
}
