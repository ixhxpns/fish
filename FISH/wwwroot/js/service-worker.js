self.addEventListener('install', event => {
    console.log('Service Worker installing.');
    // 可以在這裡預緩存資源
});

self.addEventListener('activate', event => {
    console.log('Service Worker activating.');
});

self.addEventListener('fetch', event => {
    console.log('Fetching:', event.request.url);
    // 可以在這裡處理資源請求
});
