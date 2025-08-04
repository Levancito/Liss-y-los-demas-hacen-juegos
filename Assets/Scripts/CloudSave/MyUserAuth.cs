using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class MyUserAuth : MonoBehaviour
{
    public static MyUserAuth Instance { get; private set; }

    public event System.Action OnAuthenticationComplete;

    private bool isSigningIn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public async void StartSignInAsync() 
    {
        if (!PlayerAccountService.Instance.IsSignedIn)
        {
            SignInWithUnityAsync();
            return;
        }

        try
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log($"Unity services initialization: {UnityServices.State}");

        Debug.Log($"Session Token Exists: {AuthenticationService.Instance.SessionTokenExists}");

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Access Token : {AuthenticationService.Instance.AccessToken}");
            OnAuthenticationComplete?.Invoke();
        };

        AuthenticationService.Instance.SignInFailed += errorResponse =>
        {
            Debug.LogError($"Sign In failed with error code: {errorResponse.ErrorCode}");
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log($"Player Signed Out");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log($"Player session expired");
        };

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await SignInAnonymouslyAsync();
        }

        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
        }

        PlayerAccountService.Instance.SignedIn += SignInWithUnityAsync;

    }

    public async Task SignInAnonymouslyAsync()
    {
        if (isSigningIn || AuthenticationService.Instance.IsSignedIn) return;

        isSigningIn = true;

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Anonymous sign in successful");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            SetStatus("Anonymous sign in failed");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            SetStatus("Anonymous sign in failed");
        }
        finally
        {
            isSigningIn = false;
        }
    }

    async void SignInWithUnityAsync()
    {
        SignOut();
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
            Debug.Log("SignIn successful");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    void ClearSessionToken()
    {
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            try
            {
                AuthenticationService.Instance.ClearSessionToken();
                SetStatus("Session token cleared");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                SetStatus("Failed to clear session token");
            }
        }
    }

    public void SignOut()
    {
        AuthenticationService.Instance.SignOut();
        SetStatus("Signed out");
        ClearSessionToken();
    }

    void SetStatus(string status) => Debug.Log($"Status: {status}");
}