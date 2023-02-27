<script lang="ts">
  import type { ExtendedRoutes, Runner } from "$lib/dtos";

  export let index: number;
  export let route: ExtendedRoutes;
  export let runners: Runner[];

  export let update: () => void;
</script>

<tr>
  <th>{index + 1}</th>
  <td>{route.distance}</td>
  <td>{route.startingLocation} <br>=> {route.arrivalLocation}</td>
  <td>{route.locationName}</td>
  <td>
    <select class="select select-ghost w-36 max-w xs" bind:value={route.runnerIndex} on:change={update}>
      {#each runners as runner, i}
        <option value={i}>{runner.firstName} {runner.lastName}</option>
      {/each}
    </select>
  </td>
  <td>
    {(Math.floor(route.time / 60) % 60).toString().padStart(2, "0")}
    :
    {(route.time % 60).toString().padStart(2, "0")}
  </td>
  <td>
    {Math.floor(route.timeTotal / 3600).toString().padStart(2, "0")}
    :
    {(Math.floor(route.timeTotal / 60) % 60).toString().padStart(2, "0")}
    :
    {(route.timeTotal % 60).toString().padStart(2, "0")}
  </td>
</tr>
