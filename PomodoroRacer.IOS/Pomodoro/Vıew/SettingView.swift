//
//  SettingView.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 20.06.23.
//

import SwiftUI

struct SettingView: View {
    @StateObject private var vm = CounterDownViewModel()
    
    var body: some View {
        NavigationStack {
            Grid {
                GridRow {
                    HStack {
                        Text(String(localized: "worktime-string"))
                            .bold()
                            .font(.title)
                        Spacer()
                        TextField(String(localized: "worktime-string"), text: $vm.workTime)
                            .textFieldStyle(.roundedBorder)
                            .frame(width: 40)
                    }
                }
                GridRow {
                    HStack {
                        Text(String(localized: "short-break"))
                            .bold()
                            .font(.title)
                        Spacer()
                        TextField(String(localized: "short-break"), text: $vm.shortBreak)
                            .textFieldStyle(.roundedBorder)
                            .frame(width: 40)
                    }
                }
                GridRow {
                    HStack {
                        Text(String(localized: "long-break"))
                            .bold()
                            .font(.title)
                        Spacer()
                        TextField(String(localized: "long-break"), text: $vm.longBreak)
                            .textFieldStyle(.roundedBorder)
                            .frame(width: 40)
                    }
                }
            }
            .padding()
            .navigationTitle(String(localized: "setting-string"))
            .toolbarColorScheme(.dark, for: .navigationBar)
            .toolbarBackground(Color.red, for: .navigationBar)
            .toolbarBackground(.visible, for: .navigationBar)
        }
    }
}

struct SettingView_Previews: PreviewProvider {
    static var previews: some View {
        SettingView()
    }
}
