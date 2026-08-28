<template>
  <div class="p-6">
    <div class="bg-white shadow p-4 rounded">
      <h2 class="text-xl">Welcome {{ user.email }}</h2>
      <p>Role: {{ user.role }}</p>

      <button class="mt-3 bg-blue-500 text-white px-3 py-1">
        Download PDF
      </button>
    </div>
  </div>
</template>

<script setup>
const user = JSON.parse(localStorage.getItem("user"));
</script>