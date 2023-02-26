<script lang="ts">
  import { sendAuthRequest } from "$lib/api";
  import type { ErrorResponse } from "$lib/dtos";
  import { toast } from "@zerodevx/svelte-toast";
  import { goto } from "$app/navigation";

  let username: string;
  let password: string;

  const login = async (username: string, password: string) => {
    const authResponse = await sendAuthRequest(username, password);

    console.log(authResponse);
    if (authResponse.status === 401) {
      const errorResponse = (await authResponse.json()) as ErrorResponse;
      console.log(errorResponse);
      toast.push(errorResponse.error, {
        theme: {
          "--toastColor": "white",
          "--toastBackground": "rgba(187, 0, 0, 0.9)",
          "--toastBarBackground": "darkred",
        },
      });
    } else if (authResponse.status === 204) {
      localStorage.setItem("token", btoa(`${username}:${password}`));
      toast.push("Login succeeded!", {
        theme: {
          "--toastColor": "white",
          "--toastBackground": "rgba(70,190,130,0.9)",
          "--toastBarBackground": "#2F855A",
        },
        duration: 2000,
      });
      setTimeout(() => {
        goto("/")
      }, 1000);
    } else {
      toast.push("Something went wrong", {
        theme: {
          "--toastColor": "white",
          "--toastBackground": "red",
          "--toastBarBackground": "darkred",
        },
      });
    }
  };
</script>

<div class="hero min-h-screen bg-base-100">
  <div class="hero-content flex-col">
    <div class="lg:text-left">
      <h1 class="text-center text-5xl font-bold">UB Running Fest</h1>
      <p class="py-6">
        This is the administration website of the UB Running Fest 2023. For more
        information, please visit the <a
          class="link link-primary"
          href="https://ultrabalaton.hu/nnultrabalaton/">official website</a
        >
      </p>
    </div>
    <div class="card flex-shrink-0 w-full max-w-md shadow-2xl bg-base-100">
      <div class="card-body">
        <div class="form-control">
          <label class="label" for="username">
            <span class="label-text">Username</span>
          </label>
          <input
            id="username"
            type="text"
            placeholder="username"
            class="input input-bordered"
            bind:value={username}
          />
        </div>
        <div class="form-control">
          <label class="label" for="password">
            <span class="label-text">Password</span>
          </label>
          <input
            id="password"
            type="password"
            placeholder="password"
            class="input input-bordered"
            bind:value={password}
          />
        </div>
        <span class="link link-secondary link-hover text-right"
          >Forgotten password</span
        >
        <div class="form-control mt-6">
          <button
            class="btn btn-primary"
            on:click={() => login(username, password)}>Login</button
          >
        </div>
      </div>
    </div>
  </div>
</div>
