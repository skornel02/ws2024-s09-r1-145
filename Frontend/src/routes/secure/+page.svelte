<script lang="ts">
  import { checkAuthStatus } from "$lib/api";
  import type { PageData } from "./$types";
  import { browser } from '$app/environment';

  export let data: PageData;

  let token = "-";
  if (browser) {
    token = localStorage.getItem("token") ?? "-";
  }

  const authStatus = checkAuthStatus(token).then(
    (loggedIn) => {
      if (!loggedIn) {
        window.location.href = "/login";
      }
    }
  );

  const logout = async () => {
    localStorage.removeItem("token");
    window.location.href = "/login";
  };
</script>

<div class="hero min-h-screen bg-base-100">
  <div class="hero-content flex-col">
    {#await authStatus}
      <progress class="progress w-56" />
    {:then}
      <div class="lg:text-left">
        <h1 class="text-center text-5xl font-bold">Secure page</h1>
      </div>
      <div class="card flex-shrink-0 w-full max-w-md shadow-2xl bg-base-100">
        <div class="card-body">
          <p class="py-6">You successfully logged in!</p>
          <button class="btn btn-primary" on:click={() => logout()}
            >Logout</button
          >
        </div>
      </div>
    {/await}
  </div>
</div>
