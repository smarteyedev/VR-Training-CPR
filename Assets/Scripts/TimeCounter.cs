using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Referensi ke UI Text untuk menampilkan waktu
    public float targetTime = 10f; // Target waktu dalam detik
    private float elapsedTime = 0f; // Waktu yang telah berlalu
    private bool isRunning = false; // Apakah stopwatch sedang berjalan

    public UnityEvent OnTimeReached;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // Tambahkan waktu yang berlalu
            UpdateTimerUI();

            // Periksa apakah waktu telah mencapai target
            if (elapsedTime >= targetTime)
            {
                elapsedTime = targetTime; // Pastikan waktu berhenti tepat di target
                StopStopwatch(); // Hentikan stopwatch
                OnTargetReached(); // Panggil aksi saat target tercapai
            }
        }
    }

    public void StartStopwatch()
    {
        isRunning = true; // Mulai stopwatch
    }

    public void StopStopwatch()
    {
        isRunning = false; // Hentikan stopwatch
    }

    public void ResetStopwatch()
    {
        isRunning = false; // Hentikan stopwatch
        elapsedTime = 0f; // Atur ulang waktu ke 0
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Format waktu hanya dalam detik (dengan 2 desimal)
            timerText.text = $"{elapsedTime:F1} detik";
        }
    }

    private void OnTargetReached()
    {
        OnTimeReached?.Invoke();
    }
}
