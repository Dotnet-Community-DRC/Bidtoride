import type { Metadata } from 'next'
import NavBar from './nav/NavBar'
import './globals.css'
import ToasterProvider from './providers/ToasterProvider'
import SignalRProvider from './providers/SignalRProvider'
import { getCurrentUser } from './actions/authActions'

export const metadata: Metadata = {
  title: 'Bid2Ride',
  description: 'Auction car App',
}

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  const user = await getCurrentUser()
  return (
    <html lang="en">
      <body>
        <ToasterProvider />
        <NavBar />
        <main className="container mx-auto px-5 pt-10">
          <SignalRProvider user={user}>{children}</SignalRProvider>
        </main>
      </body>
    </html>
  )
}
