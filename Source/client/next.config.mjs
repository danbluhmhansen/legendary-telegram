/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  swcMinify: true,
  images: {
    domains: ['bulma.io'],
  },
  experimental: {
    images: {
      allowFutureImage: true,
    },
  },
  env: {
    serverUrl: 'https://localhost:7000/'
  },
};

export default nextConfig;
