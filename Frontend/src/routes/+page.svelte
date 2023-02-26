<script lang="ts">
  import { getRoutes, checkAuthStatus } from "$lib/api";
  import { browser } from "$app/environment";
  import { toast } from "@zerodevx/svelte-toast";
  import { goto } from "$app/navigation";
  import type { Route } from "$lib/dtos";

  let token = "-";
  if (browser) {
    token = localStorage.getItem("token") ?? "-";
  }

  const authStatus = checkAuthStatus(token).then((loggedIn) => {
    if (!loggedIn && browser) {
      toast.push("Login check failed!", {
        theme: {
          "--toastColor": "white",
          "--toastBackground": "red",
          "--toastBarBackground": "darkred",
        },
        duration: 1000,
      });
      setTimeout(() => {
        goto("/login");
      }, 1000);
    }
    return loggedIn;
  });

  const logout = async () => {
    localStorage.removeItem("token");
    goto("/login");
  };

  const routes: Promise<Route[]> = getRoutes();
</script>

{#await authStatus}
  <div class="hero min-h-screen bg-base-100">
    <div class="hero-content flex-col">
      <progress class="progress w-56" />
    </div>
  </div>
{:then authStatus}
  {#if authStatus}
    <div class="flex justify-end flex-wrap">
      <button class="btn btn-primary" on:click={() => logout()}>Logout</button>
    </div>

    <div class="flex flex-col lg:flex-row">
      <table class="table table-compact w-1/3 mx-2">
        <thead>
          <tr>
            <th>#</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Speed</th>
            <th>Total Distance</th>
          </tr>
        </thead>
        <tbody>
          {#await routes}
            <tr>
              <td colspan="5" class="text-center">
                <progress class="progress w-full" />
              </td>
            </tr>
          {:then routes}
            {#each routes as route, i}
              <tr>
                <th>{i + 1}</th>
                <td>
                  <input
                    type="text"
                    placeholder="Type here"
                    class="input input-sm input-ghost w-28 max-w-xs"
                  />
                </td>
                <td>
                  <input
                    type="text"
                    placeholder="Type here"
                    class="input input-sm input-ghost w-28 max-w-xs"
                  />
                </td>
                <td>
                  <input
                    type="text"
                    placeholder="6:00"
                    min="0"
                    class="input input-sm input-ghost w-20 max-w-xs"
                  />
                </td>
                <td>
                  <p>10</p>
                </td>
              </tr>
            {/each}
          {/await}
        </tbody>
      </table>
      <table class="table table-compact w-2/3 mx-2">
        <thead>
          <tr>
            <th>#</th>
            <th>Distance</th>
            <th>Starting Point</th>
            <th>Arriving Point</th>
            <th>Name</th>
            <th>Runner</th>
            <th>Time</th>
            <th>Time in total</th>
          </tr>
        </thead>
        <tbody>
          {#await routes}
            <tr>
              <td colspan="5" class="text-center">
                <progress class="progress w-full" />
              </td>
            </tr>
          {:then routes}
            {#each routes as route, i}
              <tr>
                <th>{i + 1}</th>
                <td>{route.distance}</td>
                <td>{route.startingLocation} </td>
                <td>{route.arrivalLocation}</td>
                <td>{route.locationName}</td>
                <td>runner</td>
                <td>6:00</td>
                <td>1:06:00</td>
              </tr>
            {/each}
          {/await}
        </tbody>
      </table>
    </div>
  {/if}
{/await}

<style>
  td {
    font-size: small;
  }
</style>
