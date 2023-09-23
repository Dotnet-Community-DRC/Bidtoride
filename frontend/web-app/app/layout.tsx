import type { Metadata } from 'next';
import NavBar from './nav/NavBar';
import './globals.css';

export const metadata: Metadata = {
  title: 'Bid2Ride',
  description: 'Auction car App',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang='en'>
      <body>
        <NavBar />
        <main className='container mx-auto px-5 pt-10'>{children}</main>
      </body>
    </html>
  );
}
