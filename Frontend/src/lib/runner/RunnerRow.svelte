<script lang="ts">
  import type { Runner } from "$lib/dtos";
  //@ts-expect-error
  import { imask } from "@imask/svelte";

  const options = {
    mask: "00:00",
    lazy: false,
  };

  export let index: number;
  export let runner: Runner;

  const initialValue = `${Math.floor(runner.speed / 60).toString().padStart(2, "0")}:${(
    runner.speed % 60
  )
    .toString()
    .padStart(2, "0")}`;

  const handleComplete = (e: CustomEvent) => {
    const speed = e.detail.value;
    const [minutes, seconds] = speed.split(":");
    const totalSeconds = Number(minutes) * 60 + Number(seconds);
    runner.speed = totalSeconds;
  };
</script>

{#key runner.rev}
  <tr>
    <th>{index + 1}</th>
    <td>
      <input
        type="text"
        bind:value={runner.firstName}
        class="input input-sm input-ghost w-28 max-w-xs"
      />
    </td>
    <td>
      <input
        type="text"
        bind:value={runner.lastName}
        class="input input-sm input-ghost w-28 max-w-xs"
      />
    </td>
    <td>
      <input
        type="text"
        class="input input-sm input-ghost w-20 max-w-xs"
        class:hidden={runner.firstName === "" && runner.lastName === ""}
        value={initialValue}
        use:imask={options}
        on:complete={handleComplete}
      />
    </td>
    <td>
      <p class:hidden={runner.firstName === "" && runner.lastName === ""}>
        {Math.round(runner.totalDistance)}
      </p>
    </td>
  </tr>
{/key}
