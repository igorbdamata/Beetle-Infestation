
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;//Biblioteca que trás o C# puro
using System.Runtime.Serialization.Formatters.Binary;//Biblioteca do binário

public static class SaveSystem
{
    public static bool ableDebugs;

    //SALVAR
    public static void SaveInt(string fileName, int value)
    {
        BinaryFormatter binary = new BinaryFormatter();//Cria um objeto do BinaryFormatter, classe que vai transformar o arquivo de texto em binário
        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";//Caminho de onde vai ser salvo, o Application.persistentDataPath faz o path receber a pasta local de save, o /player é o nome do arquivo, e o .la é o tipo, o tipo pode ser qualquer um, ele é criado
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream stream = new FileStream(path, FileMode.Create);//Ele efetivamente cria o arquivo no aminho passado anteriormente. O primeiro parâmetro é o caminho, o segundo é o que ele vai fazer, criar, deletar, etc

        binary.Serialize(stream, value);//Os parÂmetros são os arquivos que queremos transformar em binário, e os dados que queremos salvar. binary.Serialize transforma em binário
        stream.Close();//Encerra a transferência de arquivos

    }

    public static void SaveFloat(string fileName, float value)
    {
        BinaryFormatter binary = new BinaryFormatter();//Cria um objeto do BinaryFormatter, classe que vai transformar o arquivo de texto em binário

        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";//Caminho de onde vai ser salvo, o Application.persistentDataPath faz o path receber a pasta local de save, o /player é o nome do arquivo, e o .la é o tipo, o tipo pode ser qualquer um, ele é criado
        FileStream stream = new FileStream(path, FileMode.Create);//Ele efetivamente cria o arquivo no aminho passado anteriormente. O primeiro parâmetro é o caminho, o segundo é o que ele vai fazer, criar, deletar, etc

        binary.Serialize(stream, value);//Os parÂmetros são os arquivos que queremos transformar em binário, e os dados que queremos salvar. binary.Serialize transforma em binário
        stream.Close();//Encerra a transferência de arquivos
    }

    public static void SaveString(string fileName, string value)
    {
        BinaryFormatter binary = new BinaryFormatter();//Cria um objeto do BinaryFormatter, classe que vai transformar o arquivo de texto em binário

        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";//Caminho de onde vai ser salvo, o Application.persistentDataPath faz o path receber a pasta local de save, o /player é o nome do arquivo, e o .la é o tipo, o tipo pode ser qualquer um, ele é criado
        FileStream stream = new FileStream(path, FileMode.Create);//Ele efetivamente cria o arquivo no aminho passado anteriormente. O primeiro parâmetro é o caminho, o segundo é o que ele vai fazer, criar, deletar, etc

        binary.Serialize(stream, value);//Os parÂmetros são os arquivos que queremos transformar em binário, e os dados que queremos salvar. binary.Serialize transforma em binário
        stream.Close();//Encerra a transferência de arquivos
    }

    public static void SaveBool(string fileName, bool value)
    {
        BinaryFormatter binary = new BinaryFormatter();//Cria um objeto do BinaryFormatter, classe que vai transformar o arquivo de texto em binário

        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";//Caminho de onde vai ser salvo, o Application.persistentDataPath faz o path receber a pasta local de save, o /player é o nome do arquivo, e o .la é o tipo, o tipo pode ser qualquer um, ele é criado
        FileStream stream = new FileStream(path, FileMode.Create);//Ele efetivamente cria o arquivo no aminho passado anteriormente. O primeiro parâmetro é o caminho, o segundo é o que ele vai fazer, criar, deletar, etc

        binary.Serialize(stream, value);//Os parÂmetros são os arquivos que queremos transformar em binário, e os dados que queremos salvar. binary.Serialize transforma em binário
        stream.Close();//Encerra a transferência de arquivos
    }

    public static void SaveColor(string fileName, Color value)
    {
        SaveFloat(fileName + "R", value.r);
        SaveFloat(fileName + "G", value.g);
        SaveFloat(fileName + "B", value.b);
    }


    //Carregar
    public static int LoadInt(string fileName, int defaultValue = -1)
    {

        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";
        if (File.Exists(path))
        {
            //Carrega os dados
            BinaryFormatter binary = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)binary.Deserialize(stream);//binary.Deserialize transforma um arquivo binário em texto (faz o oposto do serialize), ele desincripta. O as PlayerData funciona basicamente como um GetComponent
            stream.Close();//Encerra a transferência de arquivos
            return data;

        }
        else
        {
            //Debuga erro
            Debug.Log("O arquivo não existe! : " + Application.persistentDataPath + "/" + fileName + ".la");
            return defaultValue;
        }
    }

    public static float LoadFloat(string fileName, float defaultValue = 0)
    {

        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";
        if (File.Exists(path))
        {
            //Carrega os dados
            BinaryFormatter binary = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            float data = (float)binary.Deserialize(stream);//binary.Deserialize transforma um arquivo binário em texto (faz o oposto do serialize), ele desincripta. O as PlayerData funciona basicamente como um GetComponen
            stream.Close();//Encerra a transferência de arquivos
            return data;

        }
        else
        {
            //Debuga erro
            Debug.Log("O arquivo não existe! : " + Application.persistentDataPath + "/" + fileName + ".la");
            return defaultValue;
        }
    }

    public static string LoadString(string fileName, string defaultValue = "")
    {

        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";
        if (File.Exists(path))
        {
            //Carrega os dados
            BinaryFormatter binary = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = (string)binary.Deserialize(stream);//binary.Deserialize transforma um arquivo binário em texto (faz o oposto do serialize), ele desincripta. O as PlayerData funciona basicamente como um GetComponent
            stream.Close();//Encerra a transferência de arquivos
            return data;
        }
        else
        {
            //Debuga erro
            Debug.Log("O arquivo não existe! : " + Application.persistentDataPath + "/" + fileName + ".la");
            return defaultValue;
        }
    }

    public static bool LoadBool(string fileName, bool defaultValue = false)
    {
        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";
        if (File.Exists(path))
        {
            //Carrega os dados
            BinaryFormatter binary = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            bool data = (bool)binary.Deserialize(stream);//binary.Deserialize transforma um arquivo binário em texto (faz o oposto do serialize), ele desincripta. O as PlayerData funciona basicamente como um GetComponent
            stream.Close();//Encerra a transferência de arquivos
            return data;

        }
        else
        {
            //Debuga erro
            Debug.Log("O arquivo não existe! : " + Application.persistentDataPath + "/" + fileName + ".la");
            return defaultValue;
        }
    }

    public static Color LoadColor(string fileName, Color defaultColor)
    {
        string path = Application.persistentDataPath + "/" + Criptografy(fileName + "R") + ".la";
        if (File.Exists(path))
        {
            float r = LoadFloat(fileName + "R");
            float g = LoadFloat(fileName + "G");
            float b = LoadFloat(fileName + "B");
            return new Color(r, g, b, 1);
        }
        return defaultColor;
    }

    //Divers
    public static void EraseFile(string fileName)
    {
        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";
        File.Delete(path);
    }
    public static bool FileExists(string fileName)
    {
        string path = Application.persistentDataPath + "/" + Criptografy(fileName) + ".la";
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //Menu itens
    //   [MenuItem("SaveSystem/EraseAll")]
    public static void EraseAllFiles()
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
    }


    //Codify
    public static string newText;
    private static string Criptografy(string text)
    {
        newText = "";
        string[] characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " ", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "(", ")", ",", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Y" };
        List<string> chars = new List<string>();
        chars.AddRange(characters);
        string[] codedCharacters = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        List<string> codedChars = new List<string>();
        codedChars.AddRange(codedCharacters);
        if (text[text.Length - 1].ToString() == "")//Se tiver codificado
        {
            Debug.Log("Texto está codificado");
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < characters.Length; j++)
                {
                    if (text[i] == characters[j][0])
                    {
                        newText += characters[j];
                    }
                }
            }

        }
        else//Se tiver decodificado 
        {
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < characters.Length; j++)
                {
                    if (text[i] == characters[j][0])
                    {
                        newText += codedCharacters[j];
                    }
                }
            }
        }
        return newText;
    }
}
