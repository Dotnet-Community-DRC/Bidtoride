import NextAuth, { NextAuthOptions } from 'next-auth';
import DuendeIDS6Provider from 'next-auth/providers/duende-identity-server6';

export const authOptions: NextAuthOptions = {
  session: {
    strategy: 'jwt',
  },
  providers: [
    DuendeIDS6Provider({
      id: 'id-server',
      clientId: 'nextApp',
      clientSecret: 'mysecret',
      issuer: 'http://localhost:5005',
      authorization: { params: { scope: 'openid profile bidtoride' } },
      idToken: true,
    }),
  ],
  callbacks: {
    async jwt({ token, profile, account, user }) {
      if (profile) {
        token.username = profile.username;
      }
      return token;
    },

    async session({ session, token }) {
      if (token) {
        session.user.username = token.username;
      }
      return session;
    },
  },
};

const handler = NextAuth(authOptions);

export { handler as GET, handler as POST };
