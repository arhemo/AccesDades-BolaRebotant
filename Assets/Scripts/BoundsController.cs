using System;
using System.IO;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
    public int bounds = 0;

    private string saveFolder;
    private string savePath;

    public event Action<int> OnBoundsChanged;

    private void Awake()
    {
        saveFolder = Path.Combine(Application.persistentDataPath, "Saves");
        savePath = Path.Combine(saveFolder, "bounds.json");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bounds++;
            OnBoundsChanged?.Invoke(bounds);
        }
    }

    public void SaveBounds()
    {
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        BoundsSaveData data = new BoundsSaveData();
        data.bounds = bounds;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Rebotes guardados: " + bounds);
        Debug.Log("Archivo: " + savePath);
    }

    public void LoadBounds()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No existe ningún archivo de guardado.");
            return;
        }

        string json = File.ReadAllText(savePath);

        BoundsSaveData data = JsonUtility.FromJson<BoundsSaveData>(json);

        bounds = data.bounds;

        OnBoundsChanged?.Invoke(bounds);

        Debug.Log("Rebotes cargados: " + bounds);
    }
}