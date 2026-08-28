<template>
  <div class="p-6">

    <h2 class="text-xl font-bold mb-4">Users Management</h2>

    <div class="bg-white shadow rounded p-4 overflow-x-auto">

      <!-- TABLE -->
      <table class="w-full border-collapse">

        <thead>
          <tr class="bg-gray-100 text-left">
            <th class="p-3 border">ID</th>
            <th class="p-3 border">Name</th>
            <th class="p-3 border">Email</th>
            <th class="p-3 border">Role</th>
            <th class="p-3 border">Manager</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="user in users"
            :key="user.id"
            class="hover:bg-gray-50"
          >
            <td class="p-3 border">{{ user.id }}</td>
            <td class="p-3 border">{{ user.name }}</td>
            <td class="p-3 border">{{ user.email }}</td>
            <td class="p-3 border">
              <span class="bg-blue-100 text-blue-700 px-2 py-1 rounded">
                {{ user.roleName }}
              </span>
            </td>
            <td class="p-3 border">
              {{ user.managerName || "-" }}
            </td>
          </tr>
        </tbody>

      </table>

      <!-- EMPTY STATE -->
      <p v-if="users.length === 0" class="text-center text-gray-500 mt-4">
        No users found
      </p>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useAuthStore } from "../store/auth";

const store = useAuthStore();

const users = ref([]);

// LOAD USERS
const loadUsers = async () => {
  try {
    const res = await store.getUsers(); // API call
    users.value = res;
  } catch (err) {
    console.error("ERROR LOADING USERS:", err);
  }
};

onMounted(() => {
  loadUsers();
});
</script>