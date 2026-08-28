<template>
  <nav class="bg-blue-600 text-white flex justify-between p-4">

    <!-- LEFT -->
    <div class="font-bold">
      RBAC APP
    </div>

    <!-- CENTER LINKS (ONLY WHEN LOGGED IN) -->
    <div v-if="isLoggedIn" class="space-x-4">
      <router-link to="/dashboard">Dashboard</router-link>
      <router-link to="/users" v-if="role === 'Admin'">Users</router-link>
      <router-link to="/tasks">Tasks</router-link>
      <router-link to="/change-password">Change Password</router-link>
    </div>

    <!-- RIGHT (ONLY WHEN LOGGED IN) -->
    <div v-if="isLoggedIn" class="flex items-center gap-3">

      <!-- EMAIL -->
      <span class="bg-green-700 px-2 py-1 rounded">
        {{ email }}
      </span>

      <!-- ROLE -->
      <span class="bg-blue-800 px-2 py-1 rounded">
        {{ role }}
      </span>

      <!-- LOGOUT -->
      <button
        @click="logout"
        class="bg-red-500 px-3 py-1 rounded"
      >
        Logout
      </button>

    </div>

    <!-- OPTIONAL: SHOW WHEN LOGGED OUT -->
    <div v-else class="flex gap-3">
      <router-link to="/" class="hover:underline">Login</router-link>
      <router-link to="/register" class="hover:underline">Register</router-link>
    </div>

  </nav>
</template>

<script setup>
import { computed } from "vue";
import { useAuthStore } from "../store/auth";

const store = useAuthStore();

// AUTH CHECK
const isLoggedIn = computed(() => !!store.token);

//Logged In - User ID
const idLoggedInUser = computed(() => store.user?.id);

// ROLE
const role = computed(() => store.user?.role || "");

// EMAIL
const email = computed(() => store.user?.email || "");

// LOGOUT
const logout = () => {
  store.logout();
  window.location.href = "/";
};
</script>