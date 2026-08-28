<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">

    <div class="w-96 p-6 shadow-lg rounded bg-white">

      <h2 class="text-xl mb-4">Register</h2>

      <!-- Inputs -->
      <input v-model="name" placeholder="Name" class="border p-2 w-full mb-2" />
      <input v-model="email" placeholder="Email" class="border p-2 w-full mb-2" />
      <input v-model="password" type="password" placeholder="Password" class="border p-2 w-full mb-2" />
      
      <input v-model="joiningDate" type="date" class="border p-2 w-full mb-2" />

      <!-- Role -->
      <select v-model="roleId" class="border p-2 w-full mb-2">
        <option :value="1">Admin</option>
        <option :value="2">Manager</option>
        <option :value="3">Employee</option>
      </select>

      <!-- Manager (optional) -->
      <input
        v-model="managerId"
        type="number"
        placeholder="Manager Id (optional)"
        class="border p-2 w-full mb-3"
      />

      <!-- Button -->
      <button
        @click="register"
        class="bg-green-500 text-white w-full p-2 rounded"
      >
        Register
      </button>

      <!-- Messages -->
      <p v-if="message" class="text-green-600 mt-2">
        {{ message }}
      </p>

      <p v-if="error" class="text-red-500 mt-2">
        {{ error }}
      </p>

    </div>

  </div>
</template>

<script setup>
import { ref } from "vue";
import { useAuthStore } from "../store/auth";

const store = useAuthStore();

// form fields
const name = ref("");
const email = ref("");
const password = ref("");
const joiningDate = ref("");
const roleId = ref(3);
const managerId = ref(null);

// messages
const message = ref("");
const error = ref("");

// REGISTER FUNCTION
const register = async () => {
  try {
    message.value = "";
    error.value = "";

    const payload = {
      name: name.value,
      email: email.value,
      password: password.value,
      joiningDate: joiningDate.value
        ? new Date(joiningDate.value).toISOString()
        : null,
      roleId: Number(roleId.value),
      managerId: managerId.value ? Number(managerId.value) : null
    };

    const res = await store.register(payload);

    message.value = res.message || "Registration successful";

    // reset form
    name.value = "";
    email.value = "";
    password.value = "";
    joiningDate.value = "";
    roleId.value = 3;
    managerId.value = null;

  } catch (err) {
    error.value =
      err?.response?.data?.message ||
      "User already exists or invalid data";
  }
};
</script>