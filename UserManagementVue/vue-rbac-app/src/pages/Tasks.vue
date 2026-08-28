<template>
  <div class="p-6">

    <h2 class="text-xl mb-4">Assigned Tasks</h2>

    <div class="bg-white p-6 shadow rounded w-full max-w-2xl">

      <label>Title:</label>
      <input v-model="title" class="border p-2 w-full mb-2" />

      <label>Description:</label>
      <textarea v-model="description" class="border p-2 w-full mb-2"></textarea>

      <label>Status:</label>
      <select v-model="status" class="border p-2 w-full mb-2">
        <option :value="0">Pending</option>
        <option :value="1">In Progress</option>
        <option :value="2">Completed</option>
      </select>

      <label>Completion Date:</label>
      <input v-model="completionDate" type="date" class="border p-2 w-full mb-2" />

      <label>Attachment:</label>
      <input v-model="filePath" class="border p-2 w-full mb-2" />

      <label>Is Assigned:</label>
      <input type="checkbox" v-model="isAssigned" class="mb-2" />

      <!-- DYNAMIC DROPDOWN -->
      <label>User:</label>
      <select v-model="userId" class="border p-2 w-full mb-2">
        <option value="" disabled>Select User</option>
        <option v-for="u in users" :key="u.id" :value="u.id">
          {{ u.name }} (ID: {{ u.id }})
        </option>
      </select>

      <button
        @click="submitTask"
        class="bg-blue-500 text-white w-full p-2 rounded"
      >
        Submit Task
      </button>

      <p v-if="message" class="text-green-600 mt-2">{{ message }}</p>
      <p v-if="error" class="text-red-600 mt-2">{{ error }}</p>

    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import { useAuthStore } from "../store/auth";

const store = useAuthStore();

// AUTH INFO
const isLoggedIn = computed(() => !!store.token);
const idLoggedInUser = computed(() => store.user?.id);
const role = computed(() => store.user?.role || "");
const email = computed(() => store.user?.email || "");

// FORM FIELDS
const title = ref("");
const description = ref("");
const assignedBy = computed(() => store.user?.email || "");
const status = ref(0);
const completionDate = ref("");
const filePath = ref("");
const isAssigned = ref(false);
const userId = ref(null);

// USERS DROPDOWN
const users = ref([]);

// MESSAGES
const message = ref("");
const error = ref("");

// LOAD USERS BASED ON LOGGED-IN USER
onMounted(async () => {
  try {
    if (idLoggedInUser.value) {
      const res = await store.getUsersByManagerId(idLoggedInUser.value);
      users.value = res;
    }
  } catch (err) {
    console.error("ERROR LOADING USERS:", err);
  }
});

// TASK PAYLOAD
const taskPayload = computed(() => ({
  title: title.value,
  description: description.value,
  assignedBy: assignedBy.value,
  status: Number(status.value),
  completionDate: completionDate.value
    ? new Date(completionDate.value).toISOString()
    : null,
  filePath: filePath.value || null,
  isAssigned: isAssigned.value,
  userId: Number(userId.value),
}));

// SUBMIT TASK
const submitTask = async () => {
  try {
    message.value = "";
    error.value = "";

    const res = await store.createTask(taskPayload.value);

    message.value = res.message || "Task created successfully.";

    // RESET FORM
    title.value = "";
    description.value = "";
    status.value = 0;
    completionDate.value = "";
    filePath.value = "";
    isAssigned.value = false;
    userId.value = null;

  } catch (err) {
    error.value =
      err?.response?.data?.message || "Failed to create task.";
  }
};
</script>