<script setup>
import { ref } from "vue";

const count = ref(0);
</script>

<template>
  <div class="bg-gray-50 min-h-screen">

    <!-- HERO SECTION -->
    <section class="flex flex-col items-center justify-center text-center py-20 px-4 bg-gradient-to-r from-blue-600 to-indigo-700 text-white">

      <h1 class="text-4xl md:text-5xl font-bold mb-4">
        Welcome to RBAC User Management System
      </h1>

      <p class="max-w-2xl text-lg text-blue-100">
        A modern Role-Based Access Control system built with Vue 3, Tailwind CSS, and JWT Authentication.
        Manage Users, Roles, Tasks, and Permissions in one powerful dashboard.
      </p>

      <div class="mt-6 flex gap-4">
        <router-link to="/login" class="bg-white text-blue-600 px-5 py-2 rounded shadow">
          Login
        </router-link>

        <router-link to="/register" class="bg-blue-800 px-5 py-2 rounded shadow">
          Register
        </router-link>
      </div>

    </section>

    <!-- FEATURES -->
    <section class="py-16 px-6 max-w-6xl mx-auto grid md:grid-cols-3 gap-6">

      <div class="bg-white p-6 rounded shadow hover:shadow-lg transition">
        <h2 class="text-xl font-bold mb-2">🔐 Secure Authentication</h2>
        <p class="text-gray-600">
          JWT-based authentication ensures secure login, token validation, and protected routes.
        </p>
      </div>

      <div class="bg-white p-6 rounded shadow hover:shadow-lg transition">
        <h2 class="text-xl font-bold mb-2">👥 Role Management</h2>
        <p class="text-gray-600">
          Manage Admin, Manager, and Employee roles with full access control.
        </p>
      </div>

      <div class="bg-white p-6 rounded shadow hover:shadow-lg transition">
        <h2 class="text-xl font-bold mb-2">📋 Task System</h2>
        <p class="text-gray-600">
          Assign tasks, track progress (25%, 50%, 75%, 100%), and manage completion status.
        </p>
      </div>

    </section>

    <!-- ABOUT SECTION -->
    <section class="bg-white py-16 px-6">
      <div class="max-w-4xl mx-auto text-center">

        <h2 class="text-3xl font-bold mb-4">
          Why Choose This System?
        </h2>

        <p class="text-gray-600 leading-relaxed">
          This RBAC system is designed for real-world enterprise applications.
          It helps organizations manage employees, roles, and permissions efficiently.
          Built with Vue 3 Composition API, Pinia store, Tailwind CSS, and JWT authentication,
          it provides a scalable and secure foundation for modern web applications.
        </p>

      </div>
    </section>

    <!-- COUNTER DEMO (OPTIONAL REMOVE LATER) -->
    <section class="text-center py-10">
      <button
        @click="count++"
        class="bg-blue-600 text-white px-6 py-2 rounded shadow"
      >
        Click Counter: {{ count }}
      </button>
    </section>

    <!-- CTA SECTION -->
    <section class="bg-indigo-700 text-white text-center py-16 px-6">

      <h2 class="text-3xl font-bold mb-2">
        Ready to Get Started?
      </h2>

      <p class="mb-6 text-indigo-100">
        Login or register now to access your dashboard.
      </p>

      <router-link
        to="/register"
        class="bg-white text-indigo-700 px-6 py-3 rounded font-semibold"
      >
        Create Account
      </router-link>

    </section>

    <!-- FOOTER -->
    <footer class="bg-gray-900 text-white text-center py-6">
      <p>© 2026 RBAC System | Built with Vue 3 + Tailwind CSS</p>
    </footer>

  </div>
</template>