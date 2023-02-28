<script lang="ts">
  import { getRoutes, checkAuthStatus } from "$lib/api";
  import { browser } from "$app/environment";
  import { toast } from "@zerodevx/svelte-toast";
  import { goto } from "$app/navigation";
  import { getRunners, saveRunners } from "$lib/storage";
  import type { ExtendedRoutes } from "$lib/dtos";
  import RunnerRow from "$lib/runner/RunnerRow.svelte";
  import RouteRow from "$lib/route/RouteRow.svelte";

  let token = "-";
  if (browser) {
    token = localStorage.getItem("token") ?? "-";
  }

  const authStatus = checkAuthStatus(token).then((loggedIn) => {
    if (!loggedIn && browser) {
      if (token !== "-") {
        toast.push("Login check failed!", {
          theme: {
            "--toastColor": "white",
            "--toastBackground": "red",
            "--toastBarBackground": "darkred",
          },
          duration: 3000,
        });
        setTimeout(() => {
          goto("/login");
        }, 1500);
      } else {
        goto("/login");
      }
    }
    return loggedIn;
  });

  const routes = !browser
    ? undefined
    : getRoutes().then((routes) => {
        return routes.map((route) => {
          const nextRoute: ExtendedRoutes = {
            ...route,
            runnerIndex: 0,
            time: 0,
            timeTotal: 0,
          };
          return nextRoute;
        });
      });
  const runners = getRunners(browser);

  let rev = 0;

  const updateNumbers = async () => {
    console.log("Updating numbers...");
    const realRoutes = await routes;
    const realRunners = await runners;

    console.log(realRunners);

    realRunners.forEach((runner) => {
      runner.totalDistance = 0;
    });
    realRoutes!.forEach((route) => {
      realRunners[route.runnerIndex].totalDistance += route.distance;
    });

    let totalTime = 0;
    realRoutes!.forEach((route) => {
      let time = Math.round(
        route.distance * realRunners[route.runnerIndex].speed
      );
      if (
        realRunners[route.runnerIndex].firstName === "" &&
        realRunners[route.runnerIndex].lastName === ""
      ) {
        time = 0;
      }

      route.time = time;
      totalTime += route.time;
      route.timeTotal = totalTime;
    });

    rev++;
    toast.push("Recalculated!", {
      theme: {
        "--toastColor": "black",
        "--toastBackground": "lightblue",
        "--toastBarBackground": "blue",
      },
      duration: 2500,
    });
  };

  const save = async () => {
    saveRunners(runners);
    console.log("Saving...", runners);
    toast.push("Saved!", {
      theme: {
        "--toastColor": "white",
        "--toastBackground": "green",
        "--toastBarBackground": "darkgreen",
      },
      duration: 2500,
    });
  };

  const logout = async () => {
    localStorage.removeItem("token");
    goto("/login");
  };

  if (routes !== undefined) {
    routes.then(() => {
      authStatus.then((loggedIn) => {
        if (loggedIn) {
          updateNumbers();
        }
      });
    });
  }
</script>

{#await authStatus}
  <div class="hero min-h-screen bg-base-100">
    <div class="hero-content flex-col">
      <progress class="progress w-56" />
    </div>
  </div>
{:then authStatus}
  {#if authStatus}
    <div class="flex justify-center flex-wrap my-4">
      <button class="btn btn-secondary mx-2" on:click={() => save()}
        >Save</button
      >
      <button class="btn btn-info mx-2" on:click={() => updateNumbers()}
        >Recalc</button
      >
      <button class="btn btn-primary mx-2" on:click={() => logout()}
        >Logout</button
      >
    </div>

    <div class="flex flex-col lg:flex-row">
      <div class="flex justify-center lg:block">
        <table class="table table-compact w-1/3 mx-2">
          <caption> Team members </caption>
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
            {#key rev}
              {#each runners as runner, i}
                <RunnerRow {runner} index={i} update={updateNumbers} />
              {/each}
            {/key}
          </tbody>
        </table>
      </div>
      <table class="table table-compact w-2/3 mx-2">
        <caption>
          <div class="tooltip" data-tip="This section is NOT saved!">
            Route assigments <sup>(i)</sup>
          </div>
        </caption>
        <thead>
          <tr>
            <th>#</th>
            <th>Dist</th>
            <th>Start <br /> => Arrive</th>
            <th>Name</th>
            <th>Runner</th>
            <th>Time</th>
            <th>Time in total</th>
          </tr>
        </thead>
        <tbody>
          {#if routes !== undefined}
            {#await routes}
              <tr>
                <td colspan="5" class="text-center">
                  <progress class="progress w-full" />
                </td>
              </tr>
            {:then routes}
              {#each routes as route, i}
                {#key rev}
                  <RouteRow
                    index={i}
                    {route}
                    {runners}
                    update={updateNumbers}
                  />
                {/key}
              {/each}
            {/await}
          {/if}
        </tbody>
      </table>
    </div>
  {/if}
{/await}

<style>
  caption {
    font-size: 2rem;
  }
  td {
    font-size: small;
  }
</style>
