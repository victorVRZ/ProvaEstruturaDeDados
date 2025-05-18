using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeradorDeSenha : MonoBehaviour
{
    public Image bolinha1;
    public Image bolinha2;
    public Image bolinha3;
    public Image bolinha4;

    public Image[] bolinhaResultados;

    public Text totalDeTentativas;
    public Text ultimoResultado;

    public int indiceCorBolinha1;
    public int indiceCorBolinha2;
    public int indiceCorBolinha3;
    public int indiceCorBolinha4;

    public struct CorSenha
    {
        public int posicao;
        public Color[] senha;
        public int corretasNaPosicao;
        public int corretasForaDePosicao;

        public CorSenha(int posicao, Color[] senha, int corretasNaPosicao, int corretasForaDePosicao)
        {
            this.posicao = posicao;
            this.senha = senha;
            this.corretasNaPosicao = corretasNaPosicao;
            this.corretasForaDePosicao = corretasForaDePosicao;
        }
    }

    private Color[] coresPossiveis = { Color.red, Color.blue, Color.green, Color.yellow };
    private Color[] senhaSecreta = new Color[4];
    private Color[] tentativaAtual = new Color[4];
    private int[] indicesCoresAtuais = new int[4];

    private List<CorSenha> historicoTentativas = new List<CorSenha>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GerarSenhaSecreta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GerarSenhaSecreta()
    {
        List<Color> coresDisponiveis = new List<Color>(coresPossiveis);

        for (int i = 0; i < 4; i++)
        {
            int indiceAleatorio = Random.Range(0, coresDisponiveis.Count);
            senhaSecreta[i] = coresDisponiveis[indiceAleatorio];
            coresDisponiveis.RemoveAt(indiceAleatorio);
        }

        Debug.Log("Senha gerada: " + string.Join(", ", senhaSecreta));
    }

    public void MudarCor(GameObject o) 
    {
        Debug.Log(o);
        if (o.name == "1") 
        {
            indiceCorBolinha1++;
            if (indiceCorBolinha1 > 3) 
            {
                indiceCorBolinha1 = 0;
            }
            bolinha1.color = coresPossiveis[indiceCorBolinha1];
            tentativaAtual[0] = coresPossiveis[indiceCorBolinha1];
        }
        if (o.name == "2")
        {
            indiceCorBolinha2++;
            if (indiceCorBolinha2 > 3)
            {
                indiceCorBolinha2 = 0;
            }
            bolinha2.color = coresPossiveis[indiceCorBolinha2];
            tentativaAtual[1] = coresPossiveis[indiceCorBolinha2];
        }
        if (o.name == "3")
        {
            indiceCorBolinha3++;
            if (indiceCorBolinha3 > 3)
            {
                indiceCorBolinha3 = 0;
            }
            bolinha3.color = coresPossiveis[indiceCorBolinha3];
            tentativaAtual[2] = coresPossiveis[indiceCorBolinha3];
        }
        if (o.name == "4")
        {
            indiceCorBolinha4++;
            if (indiceCorBolinha4 > 3)
            {
                indiceCorBolinha4 = 0;
            }
            bolinha4.color = coresPossiveis[indiceCorBolinha4];
            tentativaAtual[3] = coresPossiveis[indiceCorBolinha4];
        }
    }
    public void VerificarTentativa()
    {
        int corretasNaPosicao = 0;
        int corretasForaDePosicao = 0;

        bool[] senhaUsada = new bool[4];
        bool[] tentativaUsada = new bool[4];

        for (int i = 0; i < 4; i++)
        {

            if (tentativaAtual[i] == senhaSecreta[i])
            {
                corretasNaPosicao++;
                bolinhaResultados[i].color = Color.white;
                senhaUsada[i] = true;
                tentativaUsada[i] = true;
            }
            else 
            {
                bolinhaResultados[i].color = Color.black;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (tentativaUsada[i]) continue;

            for (int j = 0; j < 4; j++)
            {
                if (!senhaUsada[j] && tentativaAtual[i] == senhaSecreta[j])
                {
                    corretasForaDePosicao++;
                    senhaUsada[j] = true;
                    break;
                }
            }
        }

        CorSenha tentativa = new CorSenha(historicoTentativas.Count, tentativaAtual, corretasNaPosicao, corretasForaDePosicao);
        historicoTentativas.Add(tentativa);

        string ultimoResult = "";

        for (int i = 0; i < tentativaAtual.Length; i++) 
        {
            if (tentativaAtual[i] == Color.red) 
            {
                ultimoResult += "vermelho "; 
            }
            if (tentativaAtual[i] == Color.blue)
            {
                ultimoResult += "azul ";
            }
            if (tentativaAtual[i] == Color.green)
            {
                ultimoResult += "verde ";
            }
            if (tentativaAtual[i] == Color.yellow)
            {
                ultimoResult += "amarelo ";
            }
        }

        totalDeTentativas.text = $"numero total de tentativas = {historicoTentativas.Count}";
        ultimoResultado.text = $"ultima tentativa = {ultimoResult}";


        Debug.Log(corretasNaPosicao);
    }
}
