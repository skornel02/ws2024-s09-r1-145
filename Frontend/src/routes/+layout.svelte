<script>
  import { SvelteToast } from '@zerodevx/svelte-toast'
  import "../app.css";
</script>

<main>
  <slot />
</main>

<SvelteToast />
