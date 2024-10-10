import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  css: {
    preprocessorOptions: {
      less: {
        math: "always",
        charset: false,
        relativeUrls: true,
        javascriptEnabled: true,
        additionalData: '@import "./src/styles/global.less";',
      },
    },
  },
});
