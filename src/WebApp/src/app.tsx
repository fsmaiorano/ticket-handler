import './global.css'

import { QueryClientProvider } from '@tanstack/react-query'
import { Helmet, HelmetProvider } from 'react-helmet-async'
import { RouterProvider } from 'react-router-dom'
import { Toaster } from 'sonner'

import { ThemeProvider } from './components/theme/theme-provider'
import { queryClient } from './lib/react-query'
import { router } from './routes'

export function App() {
  return (
    <HelmetProvider>
      <ThemeProvider storageKey="tickethandler-theme" defaultTheme="dark">
        <Helmet titleTemplate="%s | Ticket" />
        <Toaster richColors />
        {/* <AppContextProvider> */}
        <QueryClientProvider client={queryClient}>
          <RouterProvider router={router} />
        </QueryClientProvider>
        {/* </AppContextProvider> */}
      </ThemeProvider>
    </HelmetProvider>
  )
}
