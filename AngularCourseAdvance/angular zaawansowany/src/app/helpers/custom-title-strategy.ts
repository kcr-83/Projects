import { Injectable } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { TitleStrategy, RouterStateSnapshot, ActivatedRouteSnapshot } from "@angular/router";

const TitlePrefix = 'Materiały szkoleniowe'

@Injectable()
export class CustomTitleStrategy extends TitleStrategy {
  private titleChain: string[] = []

  constructor(private readonly title: Title) {
    super();
  }

  override updateTitle(routerState: RouterStateSnapshot) {
    this.titleChain = []
    const title = this.buildTitle(routerState);
    this.title.setTitle(`${TitlePrefix} - ${title}`);
  }
  override getResolvedTitleForRoute(snapshot: ActivatedRouteSnapshot) {
    if (snapshot.routeConfig?.title) {
      this.titleChain.push(snapshot.routeConfig.title as string)
    }
    return this.titleChain.join(' - ')
  }
}