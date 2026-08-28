<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">

    <div class="w-96 p-6 shadow-lg rounded bg-white">
      <h2 class="text-xl mb-4">Login</h2>

      <input v-model="email" placeholder="Email" class="border p-2 w-full mb-3" />
      <input v-model="password" type="password" placeholder="Password" class="border p-2 w-full mb-3" />

      <button @click="login" class="bg-blue-500 text-white w-full p-2">
        Login
      </button>

      <p class="text-red-500 mt-2">{{ error }}</p>
    </div>

  </div>
</template>

<script setup>
import { ref } from "vue";
import { useAuthStore } from "../store/auth";

const email = ref("");
const password = ref("");
const error = ref("");
const store = useAuthStore();

const login = async () => {
  try {
    await store.login(email.value, password.value);
    window.location.href = "/dashboard";
  } catch (e) {
    error.value = "Invalid email or password";
  }
};
</script>