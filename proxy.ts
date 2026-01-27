import { NextResponse, type NextRequest } from "next/server";

export async function proxy(request: NextRequest) {
  const sessionCookie = request.nextUrl.searchParams.get('sessionCookie');
  if (!sessionCookie) {
    return;
  }

  const response = NextResponse.next();
  response.cookies.set('__session', sessionCookie as string, {
    httpOnly: true,
    partitioned: true,
    sameSite: 'none',
    secure: true,
  });
  return response;
}

export const config = {
  matcher: [
    "/",
  ],
};