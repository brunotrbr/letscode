FROM node:16-alpine

COPY package.json .
COPY yarn.lock .
RUN yarn install

COPY public/ ./public/
COPY src/ ./src/

EXPOSE 3000
CMD ["yarn", "start"]