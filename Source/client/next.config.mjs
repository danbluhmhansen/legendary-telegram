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
};

export default nextConfig;